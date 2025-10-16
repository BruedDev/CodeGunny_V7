<?php
if(!defined('IN_DISCUZ')) {
	exit('Access Denied');
}
$ddtscfg=$_G['cache']['plugin']['ddts'];
if(!$_G['uid'] && $ddtscfg['guesten']=='0') {
	showmessage('not_loggedin', NULL, array(), array('login' => 1));
}
$navtitle = lang('plugin/ddts', 'title');
include_once libfile('function/feed');
$randfeed=mt_rand(1,3);
$icon = 'ddts';
$title_template = array(
	1=>'{actor} '.lang('plugin/ddts', 'feed1'), 
	2=>'{actor} '.lang('plugin/ddts', 'feed2'), 
	3=>'{actor} '.lang('plugin/ddts', 'feed3')
);

feed_add($icon, $title_template[$randfeed]);

include template('ddts:index');
?>