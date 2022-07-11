package Teacher;

import Database.DbConnection;
import Publics.PublicFunctions;

import javax.swing.*;
import javax.swing.event.ListSelectionEvent;
import javax.swing.event.ListSelectionListener;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

public class TeacherComfirmCourses {
    private JTable table_ComfirmGrades;
    private JButton button_Back;
    private JButton button_Comfirm;
    private JPanel mainPanel;

    public TeacherComfirmCourses() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280, 720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width / 2 - frame.getSize().width / 2, dim.height / 2 - frame.getSize().height / 2);

        table_ComfirmGrades.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);

        frame.setVisible(true);

        fillData();


        table_ComfirmGrades.getSelectionModel().addListSelectionListener(new ListSelectionListener() {
            public void valueChanged(ListSelectionEvent event) {
                String ID = table_ComfirmGrades.getValueAt(table_ComfirmGrades.getSelectedRow(), 0).toString();
            }
        });


        button_Back.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {

                TeacherMainMenu win = new TeacherMainMenu();
                frame.dispose();
            }
        });
        button_Comfirm.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try {
                    String query = "Update Grades set IsAgreed = 1 where ID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setInt(1, Integer.parseInt(table_ComfirmGrades.getValueAt(table_ComfirmGrades.getSelectedRow(), 0).toString()));
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                }

                TeacherComfirmCourses win = new TeacherComfirmCourses();
                frame.dispose();
            }
        });
    }

    void fillData() {
        try {
            String query = "Select Grades.ID, Grades.StudentID,Students.name, Students.surname, Grades.CourseID, Courses.Name, Courses.Semester , Courses.Credit from Grades Left Join Students on Grades.StudentID = Students.ID Left Join Courses ON Grades.CourseID = Courses.ID where Grades.IsTaken = 1 And Grades.IsAgreed = 0 And Grades.IsFinished = 0 And Grades.IsPassed = 0";
            Connection con = DbConnection.ConnectionDB();
            PreparedStatement pst = con.prepareStatement(query);
            ResultSet rs = pst.executeQuery();
            DbConnection.resultSetToTableModel2(rs, table_ComfirmGrades);
            pst.close();
            rs.close();
            con.close();
        } catch (Exception x) {
            PublicFunctions.infoBox(x.toString(), "error");
        }
    }
}
