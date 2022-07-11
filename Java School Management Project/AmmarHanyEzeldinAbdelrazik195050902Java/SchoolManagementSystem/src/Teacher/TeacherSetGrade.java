package Teacher;

import Database.DbConnection;
import Publics.PublicFunctions;
import Publics.PublicVariables;

import javax.swing.*;
import javax.swing.event.ListSelectionEvent;
import javax.swing.event.ListSelectionListener;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

public class TeacherSetGrade {
    private JPanel mainPanel;
    private JButton button_Back;
    private JTable table_Grades;
    private JTextField text_Midterm;
    private JTextField text_Final;
    private JButton button_Save;
    private JButton button_Finish;
    private JButton button_Failed;
    private JButton button_Passed;
    private JTextField text_Grade;

    public TeacherSetGrade() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280, 720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width / 2 - frame.getSize().width / 2, dim.height / 2 - frame.getSize().height / 2);

        table_Grades.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);

        frame.setVisible(true);

        fillData();


        table_Grades.getSelectionModel().addListSelectionListener(new ListSelectionListener() {
            public void valueChanged(ListSelectionEvent event) {
                try{

                    String ID = table_Grades.getValueAt(table_Grades.getSelectedRow(), 0).toString();
                    text_Midterm.setText( table_Grades.getValueAt(table_Grades.getSelectedRow(), 6).toString());
                    text_Final.setText( table_Grades.getValueAt(table_Grades.getSelectedRow(), 7).toString());
                    text_Grade.setText( table_Grades.getValueAt(table_Grades.getSelectedRow(), 10).toString());
                }catch (Exception x){
                    text_Midterm.setText("");
                    text_Final.setText("");
                    text_Grade.setText( "");
                }
            }
        });


        button_Back.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {

                TeacherMainMenu win = new TeacherMainMenu();
                frame.dispose();
            }
        });
        button_Save.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try {
                    String query = "Update Grades set midterm = ? , final = ? where ID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setString(1, text_Midterm.getText());
                    pst.setString(2, text_Final.getText());
                    pst.setString(3, table_Grades.getValueAt(table_Grades.getSelectedRow(), 0).toString());
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                }

                TeacherSetGrade win = new TeacherSetGrade();
                frame.dispose();
            }
        });
        button_Finish.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try {
                    String query = "Update Grades set IsFinished = 1 where ID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setString(1, table_Grades.getValueAt(table_Grades.getSelectedRow(), 0).toString());
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                }

                TeacherSetGrade win = new TeacherSetGrade();
                frame.dispose();
            }
        });
        button_Failed.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try {
                    String query = "Update Grades set IsPassed = 0 where ID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setString(1, table_Grades.getValueAt(table_Grades.getSelectedRow(), 0).toString());
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                }

                TeacherSetGrade win = new TeacherSetGrade();
                frame.dispose();
            }
        });
        button_Passed.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try {
                    String query = "Update Grades set IsPassed = 1 where ID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setString(1, table_Grades.getValueAt(table_Grades.getSelectedRow(), 0).toString());
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                }

                TeacherSetGrade win = new TeacherSetGrade();
                frame.dispose();
            }
        });
    }

    void fillData() {

        try {
            String query = "Select Grades.ID , Grades.StudentID , Students.name , Students.surname , Grades.CourseID , Courses.Name, Grades.Midterm, Grades.Final , Grades.IsFinished , Grades.IsPassed , Grades.Grade , Grades.CourseDate From Grades left join Students on Grades.StudentID = Students.ID  left join Courses ON Grades.CourseID = Courses.ID  where Grades.StudentID like ? AND Courses.TeacherID  like ?";
            Connection con = DbConnection.ConnectionDB();
            PreparedStatement pst = con.prepareStatement(query);
            pst.setInt(2, PublicVariables.teacher.getID());
            pst.setInt(1, PublicVariables.UpdateStudentID);
            ResultSet rs = pst.executeQuery();
            DbConnection.resultSetToTableModel2(rs, table_Grades);
            pst.close();
            rs.close();
            con.close();
        } catch (Exception x) {
            PublicFunctions.infoBox(x.toString(), "error");
        }
    }
}
