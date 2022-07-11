package Database;

import Publics.PublicFunctions;

import javax.swing.*;
import javax.swing.table.DefaultTableModel;
import javax.swing.table.TableCellRenderer;
import javax.swing.table.TableColumn;
import javax.swing.table.TableColumnModel;
import java.awt.*;
import java.sql.*;

public class DbConnection {
    Connection con = null;

    public static Connection ConnectionDB() {
        try {
            Class.forName("org.sqlite.JDBC");
            //C:\Users\ammar\OneDrive\Toros University\Third Year\Second Term\Final\JAVA\SchoolManagementSystem\
            String url = "jdbc:sqlite:src\\Database\\schooldb.db";
            Connection con = DriverManager.getConnection(url);
//            PublicFunctions.infoBox("Connection Succeeded","Info");
//            System.out.println("Connection Succeeded");
            return con;

        } catch (Exception x) {
            PublicFunctions.infoBox("Connection Failed\n" + x, "Connection Failed at ConnectionDB Class");
        }
        return null;
    }


    //Old Method
    public static void resultSetToTableModel(ResultSet rs, JTable table) throws SQLException {
        //Create new table model
        DefaultTableModel tableModel = new DefaultTableModel();

        //Retrieve meta data from ResultSet
        ResultSetMetaData metaData = rs.getMetaData();

        //Get number of columns from meta data
        int columnCount = metaData.getColumnCount();

        //Get all column names from meta data and add columns to table model
        for (int columnIndex = 1; columnIndex <= columnCount; columnIndex++) {
            tableModel.addColumn(metaData.getColumnLabel(columnIndex));
        }

        //Create array of Objects with size of column count from meta data
        Object[] row = new Object[columnCount];

        //Scroll through result set
        while (rs.next()) {
            //Get object from column with specific index of result set to array of objects
            for (int i = 0; i < columnCount; i++) {
                row[i] = rs.getObject(i + 1);
            }
            //Now add row to table model with that array of objects as an argument
            tableModel.addRow(row);
        }

        //Now add that table model to your table and you are done :D
        table.setModel(tableModel);

        table.setAutoResizeMode(JTable.AUTO_RESIZE_OFF);

        for (int column = 0; column < table.getColumnCount(); column++) {
            TableColumn tableColumn = table.getColumnModel().getColumn(column);
            int preferredWidth = tableColumn.getMinWidth();
            int maxWidth = tableColumn.getMaxWidth();

            for (int row2 = 0; row2 < table.getRowCount(); row2++) {
                TableCellRenderer cellRenderer = table.getCellRenderer(row2, column);
                Component c = table.prepareRenderer(cellRenderer, row2, column);
                int width = c.getPreferredSize().width + table.getIntercellSpacing().width;
                preferredWidth = Math.max(preferredWidth, width);

                //  We've exceeded the maximum width, no need to check other rows

                if (preferredWidth >= maxWidth) {
                    preferredWidth = maxWidth;
                    break;
                }
            }

            tableColumn.setPreferredWidth(preferredWidth);
        }
    }

    //Method 2 with headers
    public static void resultSetToTableModel2(ResultSet rs, JTable table) throws SQLException {
        //Create new table model
        DefaultTableModel tableModel = new DefaultTableModel();
        //Retrieve meta data from ResultSet
        ResultSetMetaData metaData = rs.getMetaData();

        //Get number of columns from meta data
        int columnCount = metaData.getColumnCount();

        //Get all column names from meta data and add columns to table model
        for (int columnIndex = 1; columnIndex <= columnCount; columnIndex++) {
            tableModel.addColumn(metaData.getColumnLabel(columnIndex));
        }

        //Create array of Objects with size of column count from meta data
        Object[] row = new Object[columnCount];

        //Scroll through result set
        while (rs.next()) {
            //Get object from column with specific index of result set to array of objects
            for (int i = 0; i < columnCount; i++) {
                row[i] = rs.getObject(i + 1);
            }
            //Now add row to table model with that array of objects as an argument
            tableModel.addRow(row);
        }

        //Now add that table model to your table and you are done :D
        table.setModel(tableModel);

        table.setAutoResizeMode(JTable.AUTO_RESIZE_OFF);

        final TableColumnModel columnModel = table.getColumnModel();
        for (int column2 = 0; column2 < table.getColumnCount(); column2++) {
            int width = 15; // Min width
            for (int row2 = 0; row2 < table.getRowCount(); row2++) {
                TableCellRenderer renderer = table.getCellRenderer(row2, column2);
                Component comp = table.prepareRenderer(renderer, row2, column2);
                width = Math.max(comp.getPreferredSize().width + 1, width);
            }
            width = Math.max(width, table.getColumnModel().getColumn(column2).getPreferredWidth());
            if (width > 300)
                width = 300;
            columnModel.getColumn(column2).setPreferredWidth(width);
        }
    }

}


