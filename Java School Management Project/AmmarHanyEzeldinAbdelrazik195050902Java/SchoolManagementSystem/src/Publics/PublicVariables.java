package Publics;

import Models.Student;
import Models.Teacher;

public class PublicVariables {
    public static boolean LoginSuccessful = false;
    public static int LoggedInID = -1;
    public static String LoggedInUsername = "";

    public static Student student = new Student(-1,"","","","","","","",-1,"",-1,"","","");
    public static Teacher teacher = new Teacher(-1,"","","",-1,"",-1,"");

    public static int UpdateStudentID = -1;

    public static int EditGradeStudent = -1;
//    public static int EditGradeCourse = -1;

    public static void Clear() {
        LoginSuccessful = false;
        LoggedInID = -1;
        LoggedInUsername = "";
        student =  new Student(-1,"","","","","","","",-1,"",-1,"","","");
        teacher = new Teacher(-1,"","","",-1,"",-1,"");
        UpdateStudentID = -1;


    }
}
