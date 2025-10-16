<?php
$usuario= 'sa';
$pass = '6D@xcforum';
$servidor = '(local)'; 
$basedatos = 'Db_Membership';
$basedatos2 = 'Db_Tank';



$info = array('Database'=>$basedatos, 'UID'=>$usuario, 'PWD'=>$pass); 
$conexion = sqlsrv_connect($servidor, $info);  
$info1 = array('Database'=>$basedatos2, 'UID'=>$usuario, 'PWD'=>$pass); 
$conexion2 = sqlsrv_connect($servidor, $info1); 





?>