package Teacher;

import Database.DbConnection;
import Publics.PublicFunctions;
import Publics.PublicVariables;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

public class TeacherStudents {
    private JPanel mainPanel;
    private JButton button_Back;
    private JTextField searchTextField;
    private JLabel label_Search;
    private JButton button_Update;
    private JButton button_Delete;
    private JButton button_Add;
    private JTable table_Students;


    public TeacherStudents() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280, 720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width / 2 - frame.getSize().width / 2, dim.height / 2 - frame.getSize().height / 2);

        frame.setVisible(true);

        fillData();

        button_Back.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                TeacherMainMenu win = new TeacherMainMenu();
                frame.dispose();
            }
        });
        button_Update.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                int id = -1;
                try {
                    id = Integer.parseInt(table_Students.getValueAt(table_Students.getSelectedRow(), 0).toString());
                } catch (Exception x) {
                    PublicFunctions.infoBox("Please select the student", "Error");
                }
                PublicVariables.UpdateStudentID = id;
                TeacherUpdateStudent win = new TeacherUpdateStudent();
                frame.dispose();
            }
        });
        button_Delete.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                int id = -1;
                try {
                    id = Integer.parseInt(table_Students.getValueAt(table_Students.getSelectedRow(), 0).toString());
                } catch (Exception x) {
                    PublicFunctions.infoBox("Please select the student", "Error");
                }
                try {
                    Connection con = DbConnection.ConnectionDB();
                    String query = "Delete From Open_Courses where StudentID like ?";
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setInt(1, id);
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                    con = DbConnection.ConnectionDB();
                    String query2 = "Delete From Grades where StudentID like ? ";
                    PreparedStatement pst2 = con.prepareStatement(query2);
                    pst2.setInt(1, id);
                    pst2.executeUpdate();
                    pst2.close();
                    con.close();
                    con = DbConnection.ConnectionDB();
                    String query3 = "Delete From Students Where ID like ?";
                    PreparedStatement pst3 = con.prepareStatement(query3);
                    pst3.setInt(1, id);
                    pst3.executeUpdate();
                    pst3.close();
                    con.close();
                    fillData();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                }
            }
        });
        button_Add.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                TeacherAddStudent win = new TeacherAddStudent();
                frame.dispose();
            }
        });
    }


    void fillData() {
        try {

            Connection con = DbConnection.ConnectionDB();
            String query = "Select students.ID,students.name,students.surname,students.username,students.password,students.email,students.mobile,students.gender,students.birthday,Departments.name as 'Department' from Students left join Departments on Students.DepartmentID = Departments.ID";
            PreparedStatement pst = con.prepareStatement(query);
            ResultSet rs = pst.executeQuery();
            DbConnection.resultSetToTableModel2(rs, table_Students);
            con.close();

        } catch (Exception x) {
            PublicFunctions.infoBox(x.toString(), "Error");
        }
    }
}
