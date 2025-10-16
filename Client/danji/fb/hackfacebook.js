(function() {
	var f = null;
	var na = "netseer_cookie_matching netseer_image_size netseer_image_forced netseer_global_fparam netseer_query netseer_ad_frameborder netseer_ad_format netseer_page_url netseer_output_format netseer_language netseer_gl netseer_country netseer_region netseer_city netseer_hints netseer_safe netseer_encoding netseer_ad_output netseer_max_num_ads netseer_ad_channel netseer_contents netseer_adtest netseer_kw_type netseer_kw netseer_num_radlinks netseer_max_radlink_len netseer_rl_filtering netseer_rl_mode netseer_rt netseer_ad_type netseer_image_size netseer_skip netseer_page_location netseer_referrer_url netseer_ad_region netseer_ad_section netseer_bid netseer_cpa_choice netseer_cust_age netseer_cust_gender netseer_cust_interests netseer_cust_id netseer_cust_job netseer_cust_u_url netseer_sim netseer_color_bilboard netseer_banner_id netseer_network_id netseer_tracking_url netseer_tracking_url_encoded netseer_page_url_key netseer_search_current_url netseer_page_params netseer_page_url_base64 netseer_landing_page_type netseer_background_color netseer_click_target netseer_pixel_param1 netseer_pixel_param2 netseer_pixel_param3 netseer_pixel_id netseer_tag_id netseer_client_id netseer_task netseer_creative_id netseer_auction_id netseer_slot_index netseer_imp_type netseer_ext_vid netseer_advs netseer_taglink_id netseer_segment netseer_iframe_buster netseer_search_param netseer_recirculation_sites netseer_fire_on_trigger netseer_redundant_params netseer_url_pattern netseer_theme_id netseer_imp_src netseer_endpoint netseer_ad_height netseer_ad_width netseer_page_url_key netseer_debug netseer_pixel_cpa netseer_search_term netseer_visitor_cookie netseer_cookie netseer_hints netseer_bing_formcode netseer_embed_external_pixels netseer_referrer_search_term netseer_referrer_domain netseer_concept_group_id netseer_ext_params netseer_url_suffix netseer_embed_style netseer_append netseer_pilot_id netseer_rule_id netseer_enforce_protocol netseer_log_type netseer_ad_position netseer_ad_url netseer_append_location netseer_bcpm netseer_cpc netseer_first_request_date netseer_ip netseer_last_modified_time netseer_lead_params netseer_num_ads netseer_org_error_handler netseer_pcpm netseer_user_id netseer_user_tgid netseer_page_tgid netseer_user_cgid netseer_page_cgid netseer_carrier_id netseer_lat netseer_long netseer_country netseer_city netseer_zip netseer_region netseer_dma netseer_device_type netseer_platform netseer_handset_id netseer_connection netseer_device_id netseer_site_id netseer_app_id netseer_site_name netseer_app_name netseer_ext_channel netseer_aud_segment netseer_demo netseer_inv_type netseer_req_id netseer_ext_script netseer_seller_id netseer_ssl netseer_pixel_trigger_mode"
			.split(" ");

	// Uses the proper protocol of the current URL
	function h(w) {
		
		var protocol = "http:";
		
		if(w && w.location && w.location.protocol && "https:" == w.location.protocol.toString().toLowerCase()){
			 protocol = "https:";
		} else if(w && w.netseer_page_url && w.netseer_page_url.toLowerCase().substring(0, 5) == "https"){
			protocol = "https:";
		} else if(w && (w.netseer_ssl == "1" || w.netseer_ssl == "true")){
			protocol = "https:";
		}
		
		if(w && w.netseer_scriptendpoint){
			return protocol+"//"+w.netseer_scriptendpoint;
		}

		return protocol+"//ps.ns-cdn.com";
	}

	function check(b) {
		return b != null ? b : 0;
	}

	function IEVersion() {
		var iev = 100;
		if (navigator.appName == 'Microsoft Internet Explorer') {
			var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
			if (re.exec(navigator.userAgent) != null)
				iev = parseFloat(RegExp.$1);
		}
		return iev;
	}

    function embedNetseerDynamicScript() {
        var s = document.createElement('script');
        s.type = 'text/javascript';
        s.async = true;
        s.src = h(window) + "/dsatserving2/scripts/netseer_dynamic_release.js";
        // s.src = "netseer_dynamic_dev.js";

        var x = document.getElementsByTagName('script')[0];
        x.parentNode.insertBefore(s, x);
    }

	var w = window;
	if (w) {
		try {
            if (typeof netseer_task !== "undefined" && netseer_task === "in-image") {
                var dyn_opts_names = ["netseer_x_padding", "netseer_y_padding", "netseer_infinite_scroll", "netseer_refresh_timer_interval", "netseer_refresh_timer", "netseer_refresh_classes", "netseer_refresh_ids", "netseer_refresh_ids", "netseer_max_num_img", "netseer_min_img_width", "netseer_min_img_height", "netseer_num_skipped_img", "netseer_img_discovery_mode", "netseer_open_mode", "netseer_close_mode", "netseer_open_timer", "netseer_close_timer", "netseer_animation_mode", "netseer_status", "netseer_transparency", "netseer_img_class_name", "netseer_img_blk_class_name", "netseer_oc_rate", "netseer_side_margin", "netseer_header_size", "netseer_browser_mode", "netseer_scroll_recalc"];
                dyn_opts_names = dyn_opts_names.concat(na);
                var dyn_opts = {};
                for ( var i = 0; i < dyn_opts_names.length; i++) {
                    // store current options for netseer dynamic script
                    if (typeof window[dyn_opts_names[i]] !== "undefined") {
                        dyn_opts[dyn_opts_names[i]] = window[dyn_opts_names[i]];
                    }
                }
                window['netseer_dynamic_stored_options'] = dyn_opts;

                if (window.addEventListener) {
                  window.addEventListener('load', embedNetseerDynamicScript, false);
                } else if (window.attachEvent) {
                  window.attachEvent('onload', embedNetseerDynamicScript);
                }
            } else {
                var lw = w;
                var s = document.createElement('script');
                s.src = h(lw) + "/dsatserving2/scripts/render.js";
                s.type = 'text/javascript';
                s.async = 'true';
                var rw = [];
                var nsdiv;
                for ( var c = 0; c < na.length; c++)
                    // Copy into a local variable
                    rw[na[c]] = lw[na[c]];
                rw.location = lw.location;
                rw.top = lw.top;
                if (lw.netseer_task != null && lw.netseer_task != undefined)
                    rw.netseer_append_location = lw.netseer_task + Math.random();
                else
                    rw.netseer_append_location = "netseer" + Math.random();
                if (rw.netseer_task != "px") {
                    var insElement = "<ins style='";
                    if (IEVersion() > 7.0) {
                        insElement = insElement
                                + "display:inline-table;border:none;height:"
                                + check(rw.netseer_ad_height)
                                + ";margin:0;padding:0;position:relative;visibility:visible;width:"
                                + check(rw.netseer_ad_width);
                    } else {
                        insElement = insElement
                                + "display:inline;zoom:1;border:none;height:"
                                + check(rw.netseer_ad_height)
                                + ";margin:0;padding:0;position:relative;visibility:visible;width:"
                                + check(rw.netseer_ad_width);
                    }
                    insElement = insElement + "'>";
                    insElement = insElement + "<ins id='";
                    insElement = insElement + rw.netseer_append_location
                            + "' style='";
                    insElement = insElement
                            + "display:block;border:none;height:"
                            + check(rw.netseer_ad_height)
                            + ";margin:0;padding:0;position:relative;visibility:visible;width:"
                            + check(rw.netseer_ad_width);
                    insElement = insElement + "'></ins></ins>";
                    document.write(insElement);
                } else {
                    var insElement = "<ins style='";
                    if (IEVersion() > 7.0) {
                        insElement = insElement	+ "display:inline-table;border:none;height:0;width:0;;margin:0;padding:0;position:relative;visibility:hidden";
                    } else {
                        insElement = insElement	+ "display:inline;zoom:1;border:none;height:0;width:0;margin:0;padding:0;position:relative;visibility:hidden";
                    }
                    insElement = insElement + "'>";
                    insElement = insElement + "<ins id='";
                    insElement = insElement + rw.netseer_append_location + "' style='";
                    insElement = insElement	+ "display:block;border:none;height:0;width:0;margin:0;padding:0;position:relative;visibility:hidden";
                    insElement = insElement + "'></ins></ins>";
                    document.write(insElement);
                }

                s.onload = s.onreadystatechange = function() {
                    var rs = this.readyState;
                    if (rs && rs != 'complete' && rs != 'loaded')
                        return;
                    try {
                        netseerMainFunction(rw);
                    } catch (G) {
                    }
                };
                var scriptArray = document.getElementsByTagName('script');
                var ps = scriptArray[scriptArray.length - 1];
                ps.parentNode.appendChild(s);
            }
        } catch (E) {
        }

        if (typeof netseer_task === "undefined" || netseer_task !== "in-image") {
            for ( var lw = w, c = 0; c < na.length; c++) {
                if (lw[na[c]] != null) {
                    lw[na[c]] = f;
                }
            }
        }
	}
})();