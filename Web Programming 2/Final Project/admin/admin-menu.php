<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="/stylesheets/styles.css" type="text/css">
  <link rel="preconnect" href="https://fonts.gstatic.com">
  <link href="https://fonts.googleapis.com/css2?family=Fira+Sans:wght@100&display=swap" rel="stylesheet">
  <title>Admin Login 🍕</title>
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
    <div>
    <a href="/admin/admin_total_sale.php">
    <button type="submit" value="" class="btn btn-primary">Total Sale</button>
    </a>
    <a href="/admin/admin_edit_customers.php">
    <button type="submit" value="" class="btn btn-primary">Edit Customers</button>
    </a>
    <a href="/admin/admin_edit_stocks.php">
    <button type="submit" value="" class="btn btn-primary">Edit Stocks</button>
    </a>
    </div>

    <div class="overlay-color"></div>
</body>
</html>