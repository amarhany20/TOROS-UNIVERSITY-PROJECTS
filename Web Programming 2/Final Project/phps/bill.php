<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="icon" href="/favicon/favicon.ico" />
    <link rel="stylesheet" href="/stylesheets/styles.css" />
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Fira+Sans:wght@100&display=swap" rel="stylesheet">
    <title>Bill</title>
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
            $username = $_SESSION['username'];
            $customerNumber = $_SESSION['customer-number'];
            $region = $_SESSION['region'];
            echo "<h3 class=\"home-links\" style=\"display: inline; text-align: right; position: absolute; top: 145px; right: 10px;\" >$username - $customerNumber</h3>";
            ?>
        </div>
    </div>
    <?php

    $totalPrice = $_SESSION["totalPrice"];

    $discount = $_SESSION["discount"];

    $currentdate = $_SESSION["currentdate"];

    $time = 0 ;
    if($region == "toros-university"){
        $time = 10;
    }
    if($region == "pozcu"){
        $time = 20;
    }
    if($region == "mezitli"){
        $time = 30;
    }
    if($region == "others"){
        $time = 45;
    }


    echo "
    <h2>Order Complete. <br>Bill:</h2>
    <table style=\"margin: auto;border-spacing: 10px;\">
            <thead>
            <th>
            Total Price
            </th>
            <th>
            Discount
            </th>
            <th>
            Order Date
            </th>
            <th>
            Delivery Time
            </th>
            </thead>

            <tr>
            <td>
            $totalPrice
            </td>
            <td>
            $discount
            </td>
            <td>
            $currentdate
            </td>
            <td>
            $time minutes
            </td>
            </tr>
</table>
<div class=\"ordering2-overlay-color\"></div>

    ";
    session_destroy();
    
    ?>


</body>

</html>