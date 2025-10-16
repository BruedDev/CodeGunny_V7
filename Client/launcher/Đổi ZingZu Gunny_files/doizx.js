function getResultZX(a,b,c,d,e)
{
	$.ajax({
         type:"POST",
		 url:rs_action_url,
		 data:"type="+c+"&transid="+b+"&gc="+a+"&xu="+d+"&xugame="+e,
		 dataType:"json",
		 async: true,
		 timeout:30000,
		 error: function (XMLHttpRequest, textStatus, errorThrown) {
				$('#loading_info').hide();
		 },
		 success:function(data){
						if(data.status==0){
							$('#loading_info').hide();
							window.location.href = error_url+"&tranid="+b+"&func=doizxactresult&status="+data.status;
							return false;
						}
						if(data.status==-1010||data.status==-1012||data.status==-1013){
							$('#convertblackout').hide();
							$('#loading_info').hide();
							$('.convertblackout').remove();
							$('#mess_info').html('Thời gian giao dịch đã hết hạn, vui lòng <a href="javascript:void(0);" title="fresh" onclick="javascript:location.reload(true);">nhấn vào đây</a> để reload lại trang và thực hiện lại.');
							//window.location.href = succ_url;	
							return false;
						}
						if(data.status==1||data.status==4){
							window.location.href = succ_url+"&tranid="+b+"&func=doizxactresult&status="+data.status+"&xu="+d+"&xugame="+e;
							return false;
						}
						if(data.status=='ERROR'){
							$('#loading_info').hide();
							$('#convertblackout').hide();
							$('.convertblackout').remove();
							$('#mess_info').html(data.mess);
							return false;
						}
						if(data.status=="QUEUE"||data.status==-2011){
							if(count_getTrans>limit_getresult){
								$('#loading_info').hide();
								$('#convertblackout').hide();
								$('.convertblackout').remove();
								$('#mess_info').html('Giao dịch đang xử lý, <a href="'+his_convert_url+'&tranid='+b+'">nhấn vào đây</a> để kiểm tra.');
								return false;
								}
							else{
								var i="getResultZX('"+a+"','"+b+"','"+c+"','"+d+"','"+e+"')";
								setTimeout(i,waiting_getresult);
								//setTimeout(i,8e4);
								count_getTrans++;
							}
						 }
						else{
							$('#loading_info').hide();
							window.location.href = error_url+"&tranid="+b+"&func=doizxactresult";
							//$(this).myBoxy(Boxy,{type:data.type,message:data.mess});
							return false;
						}}
						});
}

function doiZXAsync(a,b,c,d,e)
{
	count_getTrans=0;
	//limit_getresult=$("#limit_getresult").val();
	//waiting_getresult=$("#waiting_getresult").val();
		var j="Bạn có chắc muốn đổi <b>"+number_format(b,0,".",",");
		j+="</b> ZingXu thành <b>"+number_format(c,0,".",",")+"</b> "+d;
		var k=0;
		var i=0;
		$('#loading_info').show();
		$('.LuaChon').append('<div id="convertblackout" class="convertblackout">&nbsp;</div>');
		$.ajax(
		  {
		  type:"POST",
		  url:action_url,
		  data:"type="+e+"&gc="+a+"&xu="+b+"&xugame="+c,
		  dataType:"json",
		  async: true,
		  timeout:30000,
		  error: function (XMLHttpRequest, textStatus, errorThrown) {
				$('#loading_info').show();
		  },
		  success: function(data){
				if(data.status == 0){		
					window.location.href = error_url+"&tranid="+data.tranid+"&func=doizx&status="+data.err;
					return false;
				}
				if(data.status == 'ERR_LOGIN'){
					window.location.href = succ_url;	
					return false;
				}
				if(data.status==-1010||data.status==-1012||data.status==-1013){
					$('#loading_info').hide();
					$('.convertblackout').remove();					
					$('#mess_info').html('Thời gian giao dịch đã hết hạn, vui lòng <a href="javascript:void(0);" title="fresh" onclick="javascript:location.reload(true);">nhấn vào đây</a> để reload lại trang và thực hiện lại.');	
					return false;
				}
				if(data.status == 1){
					$('#loading_info').show();
					return getResultZX(a,data.transactionid,e,b,c);
				}		
		  }
		})
}

function doizxngaychoi(a,b,c,d){
return checkZX(a,b,c,d,"zx2ngay")
}
function doizxgiochoi(a,b,c,d){
 return checkZX(a,b,c,d,"zx2gio")
}
function doizingxu(gc,zx,xugame,titlexugame){
	if(check_log()==true){
		return doiZXAsync(gc,zx,xugame,titlexugame,"zx2xu");
	}
	else {
		window.location.href = login_url;
		return false;
	}
}

function time(){return Math.floor((new Date).getTime()/1e3)}
function mktime(){var a=new Date,b=arguments,c=0,d=["Hours","Minutes","Seconds","Month","Date","FullYear"];
	for(c=0;c<d.length;c++)
	{
		if(typeof b[c]==="undefined"){b[c]=a["get"+d[c]]();b[c]+=c===3}else{
			b[c]=parseInt(b[c],10);
			if(isNaN(b[c])){return false}
			}
	}
	b[5]+=b[5]>=0?b[5]<=69?2e3:b[5]<=100?1900:0:0;
	a.setFullYear(b[5],b[3]-1,b[4]);
	a.setHours(b[0],b[1],b[2]);return(a.getTime()/1e3>>0)-(a.getTime()<0)
}
function addPromotion(a,b,c)
{
	var d='<img src="'+IMG_URL+'/theme2/images//zoomloader.gif" height="25" />';
	var e="Nhận Item Code thất bại. Xin vui lòng thử lại sau.";
	var f="";$("#promotion_zone").html(d);
$.ajax({
		type:"POST",url:"/zingxu/doizxact."+c+".promotion.html",data:"tranid="+a+"&xu="+b+"&gc="+c,dataType:"json",timeout:8e4,
		error:function(){
		$("#promotion_zone").html(e);return false},
		success:function(a){
		if(!a.status){$("#promotion_zone").html(e);
		return false}
		$("#promotion_zone").html(a.mess)}})
}
function checkZX(a,b,c,d,e){
var j="Bạn có chắc muốn đổi <b>"+number_format(b,0,".",",");
j+="</b> ZingXu thành <b>"+number_format(c,0,".",",")+"</b> "+d;
var k=0;
var l=$("#sec").val();

$(this).myBoxy(Boxy,{
	type:"confirm",message:j+"?",title:"Xác nhận thông tin",
	callback:function(){
		var d=Boxy.get(this);var f=d.options;d.hide();
		showLoading();
		$.ajax({
			type:"POST",
			url:action_url,
			data:"type="+e+"&gc="+a+"&xu="+b+"&xugame="+c+"&sec="+l,
			dataType:"json",
			async: true,
			timeout:30000,
			error:function(){
			hideLoading();
			$(this).myBoxy(Boxy,{type:"alert",message:mess_server_err})
			;return false},
		success:function(c){
		hideLoading();
		if(!c.status){
			$(this).myBoxy(Boxy,{
					type:"alert",message:mess_server_err});return false}
			if(c.status==-1001){
				$(this).myBoxy(Boxy,{
				type:"alert",message:"Thời gian giao dịch đã hết hạn, vui lòng nhấn F5 để reload lại trang và thực hiện lại."});
				return false
			}
			if(c.status==0||c.status==-2012)
			{ 
			  $(this).myBoxy(Boxy,{type:"alert",message:mess_server_err});
			  return false}
		   else{
		       if(c.status==1){$("#zx_cothedoi").html(number_format(c.zingxu,0,".",","));
				   $("#zx_khongthedoi").html(number_format(c.zingxuthuong,0,".",","));
				   $("#boxDoiZX").html(c.boxDoiZX);
				   getSurveyNapthe()}
			   }
		  if(c.status==1||c.status==2){
				if(c.transactionid!=0){tranid_track=c.transactionid;addPromotion(c.transactionid,b,a)}}}
				})}})
		}
function showLoading(){
	$("#loading_info").show();
}
function hideLoading(){
	$("#loading_info").hide();
}
function check_log(){
	return true;
	if(Session["info"]==NULL)return false;
	else return true;
}
function readCookie(name) {
    var tmp = name + "=";
    var ca = document.cookie.split(';');
    for(var i=0;i < ca.length;i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') c = c.substring(1,c.length);
        if (c.indexOf(tmp) == 0) return c.substring(tmp.length,c.length);
    }
    return null;
}
var mess_server_err='<span class="Color3">Server đang bận hoặc quá tải, vui lòng thực hiện lại sau.</span>';
var count_getTrans=0;
var limit_getresult=4;
var waiting_getresult=2e3;var tranid_track=0
