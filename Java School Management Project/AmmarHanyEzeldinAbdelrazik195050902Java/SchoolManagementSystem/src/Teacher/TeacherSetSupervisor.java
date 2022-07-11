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

public class TeacherSetSupervisor {
    private JPanel mainPanel;
    private JTextField text_Search;
    private JButton button_Back;
    private JLabel label_Search;
    private JTable table_Supervisor;
    private JButton button_Set;

    public TeacherSetSupervisor() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280, 720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width / 2 - frame.getSize().width / 2, dim.height / 2 - frame.getSize().height / 2);

        table_Supervisor.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);

        frame.setVisible(true);

        fillData();


        table_Supervisor.getSelectionModel().addListSelectionListener(new ListSelectionListener() {
            public void valueChanged(ListSelectionEvent event) {

            }
        });


        button_Back.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {

                TeacherMainMenu win = new TeacherMainMenu();
                frame.dispose();
            }
        });
        button_Set.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try {
                    String query = "Update Students Set SupervisorID = ? where ID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setString(2, table_Supervisor.getValueAt(table_Supervisor.getSelectedRow(), 0).toString());
                    pst.setInt(1, PublicVariables.teacher.getID());
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                }

                TeacherSetSupervisor win = new TeacherSetSupervisor();
                frame.dispose();
            }
        });
        text_Search.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                fillData();
            }
        });
    }

    void fillData() {

        try {
            String query = "select Students.ID , Students.name , Students.surname , Students.username , Students.email , Students.mobile, Students.gender, Students.birthday , Students.departmentID , Departments.Name , Students.SupervisorID, Teachers.name , Teachers.Surname, Students.GPA from Students Left join Teachers on Students.SupervisorID = Teachers.ID Left join Departments on Students.DepartmentID = Departments.ID where Students.name like ? or Students.surname like ? or Students.username like ? or Students.email like ? or Students.mobile like ? or Students.gender like ?";
            Connection con = DbConnection.ConnectionDB();
            PreparedStatement pst = con.prepareStatement(query);
            pst.setString(1, "%"+  text_Search.getText() + "%");
            pst.setString(2, "%"+  text_Search.getText() + "%");
            pst.setString(3, "%"+  text_Search.getText() + "%");
            pst.setString(4, "%"+  text_Search.getText() + "%");
            pst.setString(5, "%"+  text_Search.getText() + "%");
            pst.setString(6, "%"+  text_Search.getText() + "%");
            ResultSet rs = pst.executeQuery();
            DbConnection.resultSetToTableModel2(rs, table_Supervisor);
            pst.close();
            rs.close();
            con.close();
        } catch (Exception x) {
            PublicFunctions.infoBox(x.toString(), "error");
        }
    }
}
