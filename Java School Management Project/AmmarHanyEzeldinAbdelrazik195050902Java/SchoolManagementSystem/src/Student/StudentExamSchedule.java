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

public class StudentExamSchedule {
    private JTable table_ExamSchedule;
    private JPanel mainPanel;
    private JButton button_Back;

    public StudentExamSchedule() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280,720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width/2-frame.getSize().width/2, dim.height/2-frame.getSize().height/2);
        fillTable();
        frame.setVisible(true);
        button_Back.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                StudentMainMenu win = new StudentMainMenu();
                frame.dispose();

            }
        });

    }
    void fillTable() {
        try {

            Connection conn = DbConnection.ConnectionDB();
            String query = "Select Name,Date,Time from Exam_Schedule where DepartmentID like ?";
            PreparedStatement pst = conn.prepareStatement(query);
            pst.setInt(1, PublicVariables.student.getDepartmentID());
            ResultSet rs = pst.executeQuery();
            DbConnection.resultSetToTableModel2(rs, table_ExamSchedule);
        } catch (Exception x) {
            PublicFunctions.infoBox("Failed \n" + x, "Failed At Courses");
        }
    }
}
