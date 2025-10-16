$(document).ready(function() {
  if(code_err == 1013){
  	 $('#txtCaptcha').focus();
	 return;
  } 
  if(code_err == 1012){
  	 $('#txtPwd').focus();
	 return;
  }  
  $('#txtSeri').focus();
 
  var isurl=$("#isurl").val();
  if(isurl!='undefined'){
	  if(isurl!=null && isurl!=1){
		  if(gamec!='underfined' && gamec!=''){
			  var check_url=checkurl(gamec);
			  if(check_url==1){
				  napthevgvnaction(check_url);
			  }
		  }
	   }
  }
});
var count_getTrans = 0;
var limit_getresult = 3;
var old_card = '';
var _serverid = 0;
function naptheaction(){
	count_getTrans = 0;
	var seri = $('#txtSeri').val();
	var pwd = $('#txtPwd').val();
	var	captcha = $('#txtCaptcha').val();
	var	serverid = $('#cbxSeri').val();
	_serverid = serverid;
	//check curent card
	var cur_card = seri+'_'+pwd+'_'+captcha;
	if(cur_card == old_card){
		return false;	
	}
	old_card = cur_card;
	$('#mess_info').html('');	
	if(!checkCardCode(seri)){$('#mess_info').html('Số seri không hợp lệ'); return;}
	if(!checkPwd(pwd)){$('#mess_info').html('Mật mã thẻ không hợp lệ'); return;}
	$('#btnSubmit').attr("disabled", true);
	$('#btnSubmit').hide();
	$('#new_capcha').hide();
	$('#btnHistory').hide();
	$('#loading_info').show();
	$.ajax({
		type: "POST",    	
		url: action_url,
		data: "txtSeri="+seri+"&txtPwd="+pwd+"&txtCaptcha="+captcha+"&serverid="+_serverid,
		dataType: 'json',
		async: true,
		timeout: 30000, // sets timeout to 60 seconds		
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			$('#loading_info').hide();
   		},
		success: function(data){
			$('#loading_info').hide();	
			return showResult(data);		
		}
	}); 
}
function showResult(data){
	var seri = $('#txtSeri').val();		
	if(data.captcha > 3 && data.code != 'NOT_RESULT'){
		$('#captcha_block').show();
		$('#captcha_img_block').show();			
		refreshImg();
	}
	//
	if(data.code == 'ERR'){		
		$('#mess_info').html(data.mess);
		$('#btnSubmit').attr("disabled", false);
		$('#btnSubmit').show();
		$('#new_capcha').show();
		$('#btnHistory').show();
		return false;
	}
	if(data.code == 'ERR_LOGIN'){
		window.location.href = succ_url;	
		return false;
	}
	if(data.code == 'SUCC'){
		window.location.href = succ_url+"&tranid="+data.tranid+"&status=1&seri="+seri;		
		return false;
	}	
	if(data.code == 'QUEUE'){
		window.location.href = succ_url+"&tranid="+data.tranid+"&status=2&seri="+seri;
		return false;
	}		
	if(data.code == 'QUEUE_2'){
		getResultDeposit(data.tranid);	
	}				
	if(data.code == 'NOT_RESULT'){
		getResultDeposit(data.tranid);		
		return false;
	}									
	if(data.code == 'ERR_SYS'){
		window.location.href = error_url+"&tranid="+data.tranid+"&func=napthe&status="+data.err;
		return false;
	}			
}
function getResultDeposit(tranid){
	if(count_getTrans > limit_getresult){
		var seri = $('#txtSeri').val();	
		window.location.href = succ_url+"&tranid="+tranid+"&status=2&seri="+seri;
		return false;
	}
	setTimeout('getResult(\''+tranid+'\')', 2000);	
	count_getTrans++;
}
function getResult(tranid){	
	$.ajax({
		type: "POST",    	
		url: action_url,
		data: "tranid="+tranid,
		dataType: 'json',
		async: false,
		timeout: 30000, // sets timeout to 60 seconds
		error: function(){},
		success: function(data){
			return showResult(data);		
		}
	}); 
}
///////////////////////////////////////
//check Seri
function checkCardCode(cardcode) {
	var cardcode = cardcode.toUpperCase();
	var re = /^(HA|SA|MA|PH|PS|PM)[0-9]{10}$/;
	var pos = cardcode.search(re);
	if(pos == -1){
		return false;
	} else {
		return true;
	}
}
//Check Pwd
function checkPwd(str) {
	if(str.length != 9){
		return false;
	}	
	if(str.charAt(0) == ' ' || str.charAt(str.length-1) == ' ') {
		return false;	
	}	
	if(!checkSpecialChars(str)) return false;
	return true;
}
function checkSpecialChars(str){
		var re = /^[0-9a-zA-Z]*$/;
		var pos = str.search(re);                                                
		if(pos == -1){
			return false;
		} else {
			return true;
		}
}