<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="/stylesheets/styles.css" type="text/css">
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Fira+Sans:wght@100&display=swap" rel="stylesheet">
    <title>Total Sale</title>
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

            session_start();
            $username = $_SESSION['admin'];
            $customerNumber = $_SESSION['admin-id'];
            echo "<h3 class=\"home-links\" style=\"display: inline; text-align: right; position: absolute; top: 145px; right: 10px;\" >$username - $customerNumber</h3>";
            ?>
        </div>
    </div>

    <div class="overlay-color"></div>


    <div>
        <form action="" name="check_stocks" method="POST">
            start date:
            <input name="start_date" type="date">
            end date:
            <input name="end_date" type="date">
            <br><br>
            <input type="submit" name="submit" value="Show Stocks">
        </form>
    </div>

    <?php
    if (isset($_POST["submit"])) {
        //sql select by date and insert total
        $start_Date = $_POST["start_date"];
        $end_Date = $_POST["end_date"];
        $totalSale = 0;
        $grossProfit = 0;
        $netIncome = 0;
        // echo $start_Date ." = " . $end_Date;

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

        $sql = "SELECT * FROM orders where order_date
BETWEEN '$start_Date' AND '$end_Date'";
        $result = $conn->query($sql);

        if ($result->num_rows > 0) {
            // output data of each row
            while ($row = $result->fetch_assoc()) {
                $totalSale += $row["total_price"] - $row["discount"];
            }
        } else {
            // echo "0 results";
        }
        $grossProfit = $totalSale * 0.35;
        $netIncome = $totalSale * 0.20;

        $sql = "INSERT INTO profit_table (total_sale, gross_sale, net_income,`start_date`,end_date)
VALUES ('$totalSale', '$grossProfit', '$netIncome','$start_Date','$end_Date')";

        if ($conn->query($sql) === TRUE) {
            // echo "New record created successfully";
            echo "<table style=\"margin:auto;\">
            <thead>
            <th>Total Sale</th>
            <th>Gross Sale</th>
            <th>Net Income</th>
            <th>Start Date</th>
            <th>End Date</th>
            
            </thead>
            <tr>
            <td>$totalSale</td>
            <td>$grossProfit</td>
            <td>$netIncome</td>
            <td>$start_Date</td>
            <td>$end_Date</td>
            </tr>
            </table>
            </div>";
        } else {
            echo "Error: " . $sql . "<br>" . $conn->error;
        }

        $conn->close();
    }
    ?>
    <div>
    <div class="ordering2-overlay-color"></div>

</body>

</html>