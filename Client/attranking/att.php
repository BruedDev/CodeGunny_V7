<?php
header("Content-type:text/html; charset=utf8"); 
include("config.php");
$posicao = 1;
$select = sqlsrv_query($conexion2,"SELECT * FROM Sys_Users_Detail ORDER BY FightPower DESC  ");
$Query = "SELECT * FROM Sys_Users_Order";
$params = array();
$options =  array( "Scrollable" => SQLSRV_CURSOR_KEYSET );
$stmt = sqlsrv_query( $conexion2, $Query , $params, $options );
$selectod = sqlsrv_num_rows($stmt);

while($item = sqlsrv_fetch_array($select)){
	
	$user = $item['UserID'];
	
	
	
	
	
	if ($selectod <= 0){
	$insert = sqlsrv_query($conexion2,"INSERT INTO Sys_Users_Order (UserID, Repute, ReputeOffer) VALUES ($user, $posicao, $posicao) ");
	}else if ($selectod >=1){
		

		$update = sqlsrv_query("UPDATE Sys_Users_Order SET UserID = $user, Repute = $posicao, ReputeOffer = $posicao WHERE UserID = '$user' ");
		
		
	}
	$posicao = $posicao+1;
	
}
echo "<center><font color = 'red' size = '35px' ><a> 排行刷新成功! </a></font></center>";

echo "<center><font color = 'green' size = '20px' ><a> </br> 此程序由星辰製作！！！ </a></font></center>";

header("refresh:60;url = http://ddt3.xingchenforum.com:82/attranking/att.php ");//url = 'site do ranking' ... 60 = 1 minuto 60 segundos



?>
