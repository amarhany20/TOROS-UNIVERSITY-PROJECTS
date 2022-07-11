package Student;

import Main.Login_Register;
import Publics.PublicVariables;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class StudentMainMenu {

    private JPanel mainPanel;
    private JButton button_MyInfo;
    private JButton button_Announcements;
    private JButton button_CourseSelection;
    private JButton button_Courses;
    private JButton button_Grades;
    private JButton button_WeeklySchedule;
    private JButton button_ExamSchedule;
    private JLabel label_ID;
    private JLabel label_User;
    private JButton button_SignOut;

    public StudentMainMenu(){
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280,720);
        frame.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width/2-frame.getSize().width/2, dim.height/2-frame.getSize().height/2);

        frame.setVisible(true);


        label_ID.setText("ID: "+ PublicVariables.LoggedInID);
        label_User.setText("Username: "+ PublicVariables.LoggedInUsername);


        button_MyInfo.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                StudentInformation win = new StudentInformation();
                frame.dispose();

            }
        });


        button_Announcements.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                StudentAnnouncements win = new StudentAnnouncements();
                frame.dispose();
            }
        });
        button_CourseSelection.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                StudentCourseSelection win = new StudentCourseSelection();
                frame.dispose();
            }
        });
        button_Courses.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                StudentCourses win = new StudentCourses();
                frame.dispose();
            }
        });
        button_Grades.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                StudentGrades win = new StudentGrades();
                frame.dispose();
            }
        });
        button_WeeklySchedule.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                StudentWeeklySchedule2 win = new StudentWeeklySchedule2();
                frame.dispose();
            }
        });
        button_ExamSchedule.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                StudentExamSchedule win = new StudentExamSchedule();
                frame.dispose();
            }
        });
        button_SignOut.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                PublicVariables.Clear();
                Login_Register win = new Login_Register();
                frame.dispose();
            }
        });

    }
}
