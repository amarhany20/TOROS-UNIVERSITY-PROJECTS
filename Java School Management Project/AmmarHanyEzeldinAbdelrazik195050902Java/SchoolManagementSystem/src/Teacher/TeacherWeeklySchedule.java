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

public class TeacherWeeklySchedule {
    private JButton button_Back;
    private JButton button_Add;
    private JTable table_WeeklySchedule;
    private JTextField text_Day;
    private JPanel mainPanel;
    private JTextField text_Name;
    private JTextField text_Time;
    private JButton button_Delete;
    private JButton button_Update;

    public TeacherWeeklySchedule() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280, 720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width / 2 - frame.getSize().width / 2, dim.height / 2 - frame.getSize().height / 2);

        table_WeeklySchedule.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);

        frame.setVisible(true);

        fillData();


        table_WeeklySchedule.getSelectionModel().addListSelectionListener(new ListSelectionListener() {
            public void valueChanged(ListSelectionEvent event) {
                String ID = table_WeeklySchedule.getValueAt(table_WeeklySchedule.getSelectedRow(), 0).toString();
                text_Name.setText(table_WeeklySchedule.getValueAt(table_WeeklySchedule.getSelectedRow(), 1).toString());
                text_Day.setText(table_WeeklySchedule.getValueAt(table_WeeklySchedule.getSelectedRow(), 2).toString());
                text_Time.setText(table_WeeklySchedule.getValueAt(table_WeeklySchedule.getSelectedRow(), 3).toString());

            }
        });


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
                try {
                    String query = "Update Weekly_Schedule set name = ? , Day = ?, Time = ? where ID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setString(1, text_Name.getText());
                    pst.setString(2, text_Day.getText());
                    pst.setString(3, text_Time.getText());
                    pst.setInt(4, Integer.parseInt(table_WeeklySchedule.getValueAt(table_WeeklySchedule.getSelectedRow(), 0).toString()));
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                }

                TeacherWeeklySchedule win = new TeacherWeeklySchedule();
                frame.dispose();
            }
        });
        button_Delete.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try{
                    String query = "Delete From Weekly_Schedule Where ID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setInt(1,Integer.parseInt(table_WeeklySchedule.getValueAt(table_WeeklySchedule.getSelectedRow(), 0).toString()));
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                }catch (Exception x){
                    PublicFunctions.infoBox(x.toString(),"Error");
                }

                TeacherWeeklySchedule win = new TeacherWeeklySchedule();
                frame.dispose();
            }
        });
        button_Add.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try{
                    String query = "Insert into Weekly_Schedule (name,Day,Time,DepartmentID) values (?,?,?,?)";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setString(1,text_Name.getText());
                    pst.setString(2,text_Day.getText());
                    pst.setString(3,text_Time.getText());
                    pst.setInt(4, PublicVariables.teacher.getDepartmentID());
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                }catch (Exception x){
                    PublicFunctions.infoBox(x.toString(),"Error");
                }
                TeacherWeeklySchedule win = new TeacherWeeklySchedule();
                frame.dispose();
            }
        });
    }

    void fillData() {

        try {
            String query = "Select ID,Name,Day,Time From Weekly_Schedule Where DepartmentID like ?";
            Connection con = DbConnection.ConnectionDB();
            PreparedStatement pst = con.prepareStatement(query);
            pst.setInt(1,PublicVariables.teacher.getDepartmentID());
            ResultSet rs = pst.executeQuery();
            DbConnection.resultSetToTableModel2(rs, table_WeeklySchedule);
            pst.close();
            rs.close();
            con.close();
        } catch (Exception x) {
            PublicFunctions.infoBox(x.toString(), "error");
        }
    }
}
