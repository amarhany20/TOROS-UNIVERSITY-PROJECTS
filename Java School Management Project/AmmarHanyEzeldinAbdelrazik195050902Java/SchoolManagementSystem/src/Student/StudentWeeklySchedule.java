package Student;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class StudentWeeklySchedule {
    private JPanel mainPanel;
    private JButton backButton;
    private JLabel label_Monday;
    private JLabel label_Tuesday;
    private JLabel label_Wednsday;
    private JLabel label_Thursday;
    private JLabel label_Friday;
    private JLabel label_Saturday;
    private JLabel label_Sunday;
    private JLabel label_812;
    private JLabel label_15;
    private JTextArea field_Mon812;
    private JTextArea field_Tue812;
    private JTextArea field_Wed812;
    private JTextArea field_Thu812;
    private JTextArea field_Fri812;
    private JTextArea field_Sat812;
    private JTextArea field_Sun812;
    private JTextArea field_Mon15;
    private JTextArea field_Tue15;
    private JTextArea field_Wed15;
    private JTextArea field_Thu15;
    private JTextArea field_Fri15;
    private JTextArea field_Sat15;
    private JTextArea field_Sun15;


    public StudentWeeklySchedule() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280,720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width/2-frame.getSize().width/2, dim.height/2-frame.getSize().height/2);


        frame.setVisible(true);



        backButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {

                StudentMainMenu win = new StudentMainMenu();
                frame.dispose();

            }
        });
    }
}
