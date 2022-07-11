package Teacher;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class TeacherSupervisedStudents {
    private JPanel mainPanel;
    private JButton button_Back;
    private JButton button_Approve;
    private JTextField text_Search;
    private JLabel label_Search;
    private JTable teable_SupervisedStudents;
    private JTable table_TakenCourses;

    public TeacherSupervisedStudents() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280,720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width/2-frame.getSize().width/2, dim.height/2-frame.getSize().height/2);

        frame.setVisible(true);

        button_Back.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                TeacherMainMenu win = new TeacherMainMenu();
                frame.dispose();
            }
        });
    }
}
