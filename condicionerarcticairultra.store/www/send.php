<html>
<head>
<meta http-equiv="Content-Language" content="ua">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>Спасибо за заказ</title>
<link type="text/css" href="images/send.css" rel="stylesheet" charset="windows-1251"/>

<? 
// ----------------------------конфигурация-------------------------- // 
$domain = $_SERVER['SERVER_NAME'];
$adminemail="podporenko.prof@gmail.com"; // e-mail админа 

$header="From: \"Заявка на кондиціонер\" <admin@$domain>\n"; // от кого
$header.="Content-type: text/html; charset=\"utf-8\""; // кодировка

$date=date("d.m.y"); // число.месяц.год 
 
$time=date("H:i"); // часы:минуты:секунды 
 
$backurl="spasibo/index.html";  // На какую страничку переходит после отправки письма 
 
//---------------------------------------------------------------------- // 
 
  
 
// Принимаем данные с формы 
 
$name=$_POST['name']; 
  
$phone=$_POST['phone']; 

$urll=$_SERVER['SERVER_NAME'];
$url=$_SERVER['REQUEST_URI'];

$msg=" 

<p>Телефон: $phone </p>

<p>Имя: $name </p>

"; 
 
 // Отправляем письмо админу  
 
mail("$adminemail", "$to $date $time Заявка 
на кондиціонер", "$msg", "$header"); 
 
// Сохраняем в базу данных 
 
$f = fopen("message.txt", "a+"); 
 
fwrite($f," \n $date $time Заявка 
на кондиціонер"); 
 
fwrite($f,"\n $msg "); 
 
fwrite($f,"\n ---------------"); 
 
fclose($f); 
 
 
// Выводим сообщение пользователю 



echo ' ';

 print "<p></p><script><!-- 
function reload() {location = \"$backurl\"}; setTimeout('reload()', 0); 
//--></script>"; 
exit; 
 
?>






<!-- <html>
 <head>
 <meta http-equiv="Refresh" content="4; URL=http://arcticair-conditions.zzz.com.ua/" /> 
 <meta http-equiv="Content-Language" content="ua">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
 <title>Спасибо за заказ</title>
 <link type="text/css" href="images/send.css" rel="stylesheet" charset="windows-1251"/>


// // ----------------------------конфигурация-------------------------- // 
// $domain = $_SERVER['SERVER_NAME'];
// $adminemail="andriygerbut@gmail.com"; // e-mail админа 

// $subject  = "Заявка на кондиціонер!";
// $headers = "From: \"Заявка на кондиціонер\" <admin@$domain>\n"; // от кого
// $headers .="Content-type: text/html; charset=\"utf-8\""; // кодировка
// $headers .= "From: " . strip_tags($username) . "\r\n";
// $headers .= "Reply-To: ". strip_tags($username) . "\r\n";
// $headers .= "MIME-Version: 1.0\r\n";
// $headers .= "Content-Type: text/html;charset=utf-8 \r\n";

// $date=date("d.m.y"); // число.месяц.год 
 
// $time=date("H:i"); // часы:минуты:секунды 
 
// $backurl="spasibo/index.html";  // На какую страничку переходит после отправки письма 
 
// //---------------------------------------------------------------------- // 
 
  
 
// // Принимаем данные с формы 
 
// $name=$_POST['name']; 
  
// $phone=$_POST['phone']; 

// $urll=$_SERVER['SERVER_NAME'];
// $url=$_SERVER['REQUEST_URI'];

// // Формирование тела письма
// $msg  = "<html><body style='font-family:Arial,sans-serif;'>";
// $msg .= "<span> ".$to."</span> <span>".$date."</span> <span>".$time."</span>\n\n";
// $msg .= "<h2 style='font-weight:bold;border-bottom:1px dotted #ccc;'>Заявка на кондиціонер!</h2>\r\n";
// $msg .= "<p><strong>Від кого:</strong>Имя: ".$name."</p>\r\n";
// $msg .= "<p><strong>Телефон:</strong>Телефон: ".$phone."</p>\r\n";
// $msg .= "</body></html>";
// // $msg=" 

// // <p>Телефон: $phone </p>

// // <p>Имя: $name </p>

// // "; 
 
 // // Отправляем письмо админу  
 
// // mail("$adminemail", "$to $date $time Заявка 
// // на кондиціонер", "$msg", "$header"); 
 
 // if(@mail($adminemail, $subject, $msg, $headers)) {
     // echo "Sent!!";
// } 
// // Сохраняем в базу данных 
 
// $f = fopen("message.txt", "a+"); 
 
// fwrite($f," \n $date $time Заявка 
// на кондиціонер"); 
 
// fwrite($f,"\n $msg "); 
 
// fwrite($f,"\n ---------------"); 
 
// fclose($f); 
 
 
// // Выводим сообщение пользователю 



 // echo ' ';

  // print "<p></p><script><!-- 
 // function reload() {location = \"$backurl\"}; settimeout('reload()', 0); 
// </script>"; 
// exit; 
 
// ?>

