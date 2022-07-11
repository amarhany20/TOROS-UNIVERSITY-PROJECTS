<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>

<body>
    <?php
    if (!empty($_GET['login'])) {
        if ($_GET["customer-number"] == null) {
            $message = "Customer Number is empty. " . ($_GET["customer-number"]);
            echo "<script type='text/javascript'>alert('$message');</script>";
            header("refresh:0.1;url=/phps/customer-login.php");
            return;
        }
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
        $userInput = $_GET["customer-number"];
        $sql = "SELECT * FROM customers WHERE customer_number='$userInput'";
        $result = $conn->query($sql);


        if ($result->num_rows > 0) {
            // output data of each row
            while ($row = $result->fetch_assoc()) {
                $message = "Welcome " . $row["username"];
                echo "<script type='text/javascript'>alert('$message');</script>";
                session_start();
                $_SESSION['username'] = $row["username"];
                $_SESSION['customer-number'] = $row["customer_number"];
                header("refresh:0.1;url=/phps/region-and-type.php");
            }
        } else {
            $message = "Customer Number not found. If you forgot your customer number you can contact us or request it by your username. If this is your first time, you have to sign up. ";
            echo "<script type='text/javascript'>alert('$message');</script>";
            header("refresh:0.1;url=/phps/customer-login.php");
        }

        mysqli_close($conn);
    }
    ?>
</body>

</html>