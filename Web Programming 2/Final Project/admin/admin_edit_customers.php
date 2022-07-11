<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="icon" href="/favicon/favicon.ico" />
    <link rel="stylesheet" href="/stylesheets/styles.css" type="text/css">
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Fira+Sans:wght@100&display=swap" rel="stylesheet">
    <title>Edit Customers</title>
</head>

<body class="home-body">
    <div class="home-top-container">
        <h1>Welcome To Pizza Restaurant</h1>
        <h2>The Best Pizza is on the way.</h2>
        <h3>Now available in Mersin</h3>
        <div class="home-navigation-bar">
            <form name="signout" method="GET" action=""><button class="home-links" style="background-color: gray;" name="signout" type="submit" class=""> Sign out</button></form>
            <?php if (isset($_GET['signout'])) {
                session_destroy();
                header("Location: /index.php");
            } ?>
            <?php
            // Setting up connection with database Geeks
            $connection = mysqli_connect(
                "localhost",
                "root",
                "",
                "pizza"
            );

            // Check connection
            if (mysqli_connect_errno()) {
                echo "Database connection failed.";
            }

            // query to fetch Username and Password from
            // the table geek
            $query = "SELECT * FROM customers";

            // Execute the query and store the result set
            $result = mysqli_query($connection, $query);

            if ($result) {
                // it return number of rows in the table.
                $row = mysqli_num_rows($result);

                if ($row) {
                    printf("Number of Customers : " . $row);
                }
                // close the result.
                mysqli_free_result($result);
            }

            // Connection close 
            mysqli_close($connection);
            session_start();
            $username = $_SESSION['admin'];
            $customerNumber = $_SESSION['admin-id'];
            echo "<h3 class=\"home-links\" style=\"display: inline; text-align: right; position: absolute; top: 145px; right: 10px;\" >$username - $customerNumber</h3>";

            ?>
        </div>
    </div>

    <div class="overlay-color"></div>

    <div>

        <form action="" method="POST">
            Enter Customer ID: <input name="id" type="text">
            <input type="submit" name="submit">
        </form>
    </div>

    <?php

    if (isset($_POST["submit"])) {
        $id =  $_POST["id"];
        $_SESSION['customer_id_tobedeleted'] = $id;
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

        $sql = "SELECT * FROM customers where customer_number = '$id'";
        $result = $conn->query($sql);

        if ($result->num_rows > 0) {
            // output data of each row
            while ($row = $result->fetch_assoc()) {
                $sql2 = "SELECT * FROM orders where customer_number = '$id'";
                $result2 = $conn->query($sql2);
                $totalCustomerPurchases = 0;
                if ($result2->num_rows > 0) {
                    // output data of each row
                    while ($row2 = $result2->fetch_assoc()) {
                        $totalCustomerPurchases +=   $row2["total_price"] - $row2['discount'];
                    }
                }
                echo "    <form method=\"POST\" action=\"\">
<table style=\"margin: auto;\">
    <thead>
    <th>
    Customer-Number
    </th>
    <th>
    Username
    </th>
    <th>
    Address
    </th>
    <th>
    Total Orders
    </th>
    </thead>
    <tr>
    <td>
    " . $row["customer_number"] . "
    </td>
    <td>
    " . $row["username"] . "
    </td>
    <td>
    " . $row["address"] . "
    </td>";


                echo "
    <td>
    " . $totalCustomerPurchases . "
    </td>
    </tr>
   
</table> 
<input type=\"submit\" name=\"delete\" value=\"delete\" id=\"\">
</form>";
            }
        } else {
            echo "Customer not found";
        }

        $conn->close();
    }
    if (isset($_POST["delete"])) {
        $id = $_SESSION['customer_id_tobedeleted'];
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

        $sql = "DELETE FROM customers WHERE customer_number=$id";

        if ($conn->query($sql) === TRUE) {
            echo "Customer deleted successfully <br>";
        } else {
            echo "Error deleting Customer: " . $conn->error;
        }

        $sql = "DELETE FROM orders WHERE customer_number=$id";

        if ($conn->query($sql) === TRUE) {
            echo "All orders for customer has been deleted successfully";
        } else {
            echo "Error deleting orders for customers: " . $conn->error;
        }

        $conn->close();
    }
    ?>
    <div class="ordering2-overlay-color"></div>

</body>

</html>