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

public class TeacherAnnouncementManagement {
    private JPanel mainPanel;
    private JButton button_Back;
    private JTable table_Annoucements;
    private JButton button_Update;
    private JButton button_Delete;
    private JButton button_Add;
    private JTextField text_Title;
    private JTextArea textArea_Announcement;
    private JTextField text_Date;

    public TeacherAnnouncementManagement() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280, 720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width / 2 - frame.getSize().width / 2, dim.height / 2 - frame.getSize().height / 2);

        table_Annoucements.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);

        frame.setVisible(true);

        fillData();


        table_Annoucements.getSelectionModel().addListSelectionListener(new ListSelectionListener() {
            public void valueChanged(ListSelectionEvent event) {
                String ID = table_Annoucements.getValueAt(table_Annoucements.getSelectedRow(), 0).toString();
                text_Title.setText(table_Annoucements.getValueAt(table_Annoucements.getSelectedRow(), 1).toString());
                text_Date.setText(table_Annoucements.getValueAt(table_Annoucements.getSelectedRow(), 2).toString());
                textArea_Announcement.setText(table_Annoucements.getValueAt(table_Annoucements.getSelectedRow(), 3).toString());


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
                    String query = "Update Announcements Set Title = ? , Date = ? , Announcement = ? where ID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setString(1, text_Title.getText());
                    pst.setString(2, text_Date.getText());
                    pst.setString(3, textArea_Announcement.getText());
                    pst.setInt(4, Integer.parseInt(table_Annoucements.getValueAt(table_Annoucements.getSelectedRow(), 0).toString()));
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                }

                TeacherAnnouncementManagement win = new TeacherAnnouncementManagement();
                frame.dispose();
            }
        });
        button_Delete.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try{
                    String query = "Delete From Announcements Where ID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setInt(1, Integer.parseInt(table_Annoucements.getValueAt(table_Annoucements.getSelectedRow(), 0).toString()));
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                }catch (Exception x){
                    PublicFunctions.infoBox(x.toString(),"Error");
                }

                TeacherAnnouncementManagement win = new TeacherAnnouncementManagement();
                frame.dispose();
            }
        });
        button_Add.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try {
                    String query = "Insert into Announcements (title,date,TeacherID,DepartmentID,Announcement) values (?,?,?,?,?)";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setString(1, text_Title.getText());
                    pst.setString(2, text_Date.getText());
                    pst.setInt(3, PublicVariables.teacher.getID());
                    pst.setInt(4, PublicVariables.teacher.getDepartmentID());
                    pst.setString(5, textArea_Announcement.getText());
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                    return;
                }
                TeacherAnnouncementManagement win = new TeacherAnnouncementManagement();
                frame.dispose();
            }
        });
    }

    void fillData() {

        try {
            String query = "Select ID,Title,Date,Announcement from Announcements Where TeacherID like ?";
            Connection con = DbConnection.ConnectionDB();
            PreparedStatement pst = con.prepareStatement(query);
            pst.setInt(1,PublicVariables.teacher.getID());
            ResultSet rs = pst.executeQuery();
            DbConnection.resultSetToTableModel2(rs, table_Annoucements);
            pst.close();
            rs.close();
            con.close();
        } catch (Exception x) {
            PublicFunctions.infoBox(x.toString(), "error");
        }
    }
}
