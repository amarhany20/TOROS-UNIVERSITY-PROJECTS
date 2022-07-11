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

public class StudentGrades {
    private JPanel mainPanel;
    private JTextField text_Search;
    private JLabel label_Search;
    private JTable table_Grades;
    private JButton button_Back;

    public StudentGrades(){
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280,720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width/2-frame.getSize().width/2, dim.height/2-frame.getSize().height/2);
        fillTable();
        table_Grades.setDefaultEditor(Object.class, null);

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
            String query = "Select Courses.name, Grades.Midterm, Grades.Final, Grades.IsFinished, Grades.Grade, Grades.CourseDate from grades LEFT JOIN Courses on Grades.CourseID = Courses.ID Where Grades.StudentID like ? And Courses.Name like ? AND Grades.IsAgreed = 1";
            PreparedStatement pst = conn.prepareStatement(query);
            pst.setInt(1, PublicVariables.student.getID());
            pst.setString(2, "%" + text_Search.getText() + "%");
            ResultSet rs = pst.executeQuery();
            DbConnection.resultSetToTableModel2(rs, table_Grades);
        } catch (Exception x) {
            PublicFunctions.infoBox("Failed \n" + x, "Failed At Courses");
        }
    }
}
