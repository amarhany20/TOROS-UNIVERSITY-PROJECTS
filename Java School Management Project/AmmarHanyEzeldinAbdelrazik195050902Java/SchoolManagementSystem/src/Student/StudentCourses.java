package Student;

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

public class StudentCourses {
    private JPanel mainPanel;
    private JTable table_Courses;
    private JButton button_Back;
    private JTextField text_Search;
    private JLabel label_Search;

    public StudentCourses() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280, 720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width / 2 - frame.getSize().width / 2, dim.height / 2 - frame.getSize().height / 2);
        fillTable();
        frame.setVisible(true);


        button_Back.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                StudentMainMenu win = new StudentMainMenu();
                frame.dispose();
            }
        });


        text_Search.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                fillTable();

            }
        });


    }

    void fillTable() {
        try {

            Connection conn = DbConnection.ConnectionDB();
            String query = "Select Courses.ID , Courses.Name , Courses.Semester, Courses.Credit , Departments.name as 'Department' , Teachers.Name as 'Teacher Name' , Teachers.Surname as 'Teacher Surname', Grades.IsPassed from Courses \n" +
                    "left Join Departments ON Courses.DepartmentID = Departments.ID \n" +
                    "left JOIN Teachers ON  Courses.TeacherID = Teachers.ID \n" +
                    "left Join Grades ON Courses.ID = Grades.CourseID And Grades.IsPassed = 1 AND Grades.StudentID like ?\n" +
                    "Where Departments.name like ? AND Courses.Name like ?";
            PreparedStatement pst = conn.prepareStatement(query);
            pst.setInt(1, PublicVariables.student.getID());
            pst.setString(2, PublicVariables.student.getDepartment());
            pst.setString(3, "%" + text_Search.getText() + "%");
            ResultSet rs = pst.executeQuery();
            DbConnection.resultSetToTableModel2(rs, table_Courses);
        } catch (Exception x) {
            PublicFunctions.infoBox("Failed \n" + x, "Failed At Courses");
        }
    }
}
