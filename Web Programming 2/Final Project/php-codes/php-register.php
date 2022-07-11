<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>

<body>


    <?php
    function phpAlert($msg) { echo '<script type="text/javascript">alert("' . $msg . '")</script>';}
    if (!empty($_POST['register'])) {
        
        $usernameInput = $_POST["usernameInput"];
        $address = $_POST["addressInput"];
        if ($usernameInput == null) {
            header("refresh:0.1;url=/phps/customer-login.php");
            $message = "Username can't be empty. ";
            echo "<script type=\"text/javascript\"> alert(\"$message\");</script> ";
            return;
        }
        
        if ($address == null) {
            $message = "address can't be empty. ";
            echo "<script type=\"text/javascript\">alert(\"$message\");</script>";
            header("refresh:0.1;url=/phps/customer-login.php");
            return;
        }
        //Check database first
        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "pizza";

        // Create connection
        $conn = mysqli_connect($servername, $username, $password, $dbname);
        // Check connection
        if (!$conn) {
            die("Connection failed: " . mysqli_connect_error());
        }
        $sql = "SELECT * FROM customers WHERE username='$usernameInput'";
        $result = $conn->query($sql);


        if ($result->num_rows > 0) {
            // output data of each row
            $message = "username already exists. If it's yours sign in or sign up with a diffrent username";
            echo "<script type=\"text/javascript\">alert(\"$message\");</script>";
            header("refresh:0.1;url=/phps/customer-login.php");
            return;
        }


        mysqli_close($conn);

        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "pizza";

        // Create connection
        $conn = new mysqli($servername, $username, $password, $dbname);
        // Check connection
        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
           
        }

        $sql = "INSERT INTO pizza.customers (username, address) VALUES ('$usernameInput', '$address')";

        if ($conn->query($sql) === TRUE) {  $sql = "SELECT * FROM customers WHERE username='$usernameInput'";
            $result = $conn->query($sql);
    
    
            if ($result->num_rows > 0) {
                // output data of each row
                while ($row = $result->fetch_assoc()) {
                    $message = "Welcome " . $row["username"] . " You customer number is ". $row["customer_number"] . " You will sign in with that number.";
                    echo "<script type='text/javascript'>alert('$message');</script>";
                    session_start();
                    $_SESSION['username'] = $row["username"];
                    $_SESSION['customer-number'] = $row["customer_number"];
                    header("refresh:0.1;url=/phps/region-and-type.php");
                }
            }
        } else {
            echo "<script type='text/javascript'>alert('\"Error: \" . $sql . \"<br>\" . $conn->error;');</script>";
            echo"error";
            echo "'\"Error: \" . $sql . \"<br>\" . $conn->error;'";
            header("refresh:3;url=/phps/customer-login.php");
            
        }

        $conn->close();
    }
    ?>

</body>

</html>