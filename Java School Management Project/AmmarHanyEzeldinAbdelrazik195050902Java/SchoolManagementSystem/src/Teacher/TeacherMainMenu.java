package Teacher;

import Main.Login_Register;
import Publics.PublicVariables;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class TeacherMainMenu {
    private JPanel mainPanel;
    private JButton button_StudentsManagement;
    private JButton button_SetSupervisor;
    private JButton button_Grades;
    private JButton button_ComfirmCourseSelection;
    private JButton button_LogOut;
    private JButton button_CourseManagement;
    private JButton button_DepartmentManagement;
    private JButton button_FacultyManagement;
    private JButton button_AnnouncementManagement;
    private JButton TeacherOpenCourses;
    private JButton button_WeeklySchedule;
    private JButton button_ExamSchedule;

    public TeacherMainMenu() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280,720);
        frame.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width/2-frame.getSize().width/2, dim.height/2-frame.getSize().height/2);

        frame.setVisible(true);
        button_StudentsManagement.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                TeacherStudents win = new TeacherStudents();
                frame.dispose();
            }
        });
        button_SetSupervisor.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                TeacherSetSupervisor win = new TeacherSetSupervisor();
                frame.dispose();
            }
        });
        button_Grades.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                TeacherSelectStudent win = new TeacherSelectStudent();
                frame.dispose();
            }
        });

        button_LogOut.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                Login_Register win = new Login_Register();
                PublicVariables.Clear();
                frame.dispose();
            }
        });
        button_CourseManagement.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {

            }
        });
        button_CourseManagement.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                TeacherCourseManagement win = new TeacherCourseManagement();
                frame.dispose();
            }
        });
        button_DepartmentManagement.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                TeacherDepartmentManagement win = new TeacherDepartmentManagement();
                frame.dispose();
            }
        });
        button_FacultyManagement.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                TeacherFacultyManagement win = new TeacherFacultyManagement();
                frame.dispose();
            }
        });

        button_AnnouncementManagement.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                TeacherAnnouncementManagement win = new TeacherAnnouncementManagement();
                frame.dispose();

            }
        });
        TeacherOpenCourses.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                TeacherOpenCourses win = new TeacherOpenCourses();
                frame.dispose();
            }
        });
        button_ComfirmCourseSelection.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                TeacherComfirmCourses win = new TeacherComfirmCourses();
                frame.dispose();
            }
        });
        button_WeeklySchedule.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {

                TeacherWeeklySchedule win = new TeacherWeeklySchedule();
                frame.dispose();
            }
        });
        button_ExamSchedule.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {

                TeacherExamSchedule win = new TeacherExamSchedule();
                frame.dispose();
            }
        });
    }
}
