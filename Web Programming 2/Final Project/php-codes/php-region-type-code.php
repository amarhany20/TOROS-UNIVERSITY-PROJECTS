<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>

<body>

    <?php
    if (isset($_POST['submit'])) { //button clicked
        if (!isset($_POST['region']))
        {
            echo"
            <script>
                alert(\"Please select your region\");
            </script>
            ";
            header( "refresh:0.1;url=/phps/region-and-type.php" );
            return;
        }
        if(!isset($_POST['type'])){
            echo"
            <script>
                alert(\"Please select the types that you want\");
            </script>
            ";
            header( "refresh:0.1;url=/phps/region-and-type.php" );
            return;
        }
        
        $region = $_POST['region'];
        $type = $_POST['type'];
        $showPizza = false;
        $showBeverage = false;
        $showDessert = false;
        if (is_array($type) || is_object($type)) {
            foreach ($type as $typeselected) {
                if ($typeselected == "pizza") {
                    $showPizza = true;
                }
                if ($typeselected == "beverage") {
                    $showBeverage = true;
                }
                if ($typeselected == "dessert") {
                    $showDessert = true;
                }
            }
        }

        session_start();
        $_SESSION['region'] = $region;
        $_SESSION['showPizza'] = $showPizza;
        $_SESSION['showBeverage'] = $showBeverage;
        $_SESSION['showDessert'] = $showDessert;
        header( "refresh:0.1;url=/phps/ordering-menu.php" );

        // $_SESSION['username'] = $region;
        // $_SESSION['customer-number'] = $region;

        // $username = $_SESSION['username'];
        // $customerNumber = $_SESSION['customer-number'];
    }

    // echo "Please select an option";
    // die();


    ?>

</body>

</html>