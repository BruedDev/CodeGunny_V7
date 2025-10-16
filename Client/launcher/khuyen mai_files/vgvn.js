// JavaScript Document
function napthevgvnaction(url){
	count_getTrans = 0;
	var td = $('#td').val();
	
	if(url!=1) {return;}
	else{
		$.ajax({
			type: "POST",    	
			url: vgaction_url,
			data: "td="+td+"&urlvg="+url,
			dataType: 'json',
			async: true,
			timeout: 3000,		
			error: function (XMLHttpRequest, textStatus, errorThrown) {
				return;
			},
			success: function(data){
				return data.status;		
			}
		}); 
	}
}
function getParentUrl() {
    var isInIframe = (parent !== window),
        parentUrl = null;
    if (isInIframe) {
        parentUrl = document.referrer;
    }
    return parentUrl;
}
function strpos (haystack, needle, offset) {
  var i = (haystack + '').indexOf(needle, (offset || 0));
  return i === -1 ? false : i;
}
function checkurl(game){
	var url='';
	var urlvg='';
	
	//url='localhost';
	if(game=='zfish' || game=='top'){
		urlvg=getParentUrl();
		if(strpos(urlvg, 'vuigame.vn') ===false) {return 0;}
		else return 1
	}
	else if(game=='kvtm'){
		urlvg=document.referrer;
		if(strpos(urlvg,'vg') ===false) {
			if(strpos(urlvg, 'kvtm_vuigame') ===false) {return 0;}
			else return 1;
		}
		else {
			return 1;
		}
	}
	else if(game=='zg'){
		urlvg=document.referrer;
		if(strpos(urlvg, 'gamevui') ===false) {return 0;}
		else return 1;
	}
	else return 0;
  	
}