<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFrontPage.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <!--page-->
		<!-- REVOLUTION SLIDER -->
    <div class="fullwidthbanner-container">
        	<div class="fullwidthbanner">
		<ul>
		<!-- Slide 1 -->
			<li data-transition="slideright">
				<img src="img/slider/slider1.jpg" alt="" />
				
				<!-- Caption -->
				<div class="tp-caption lfr" data-x="left" data-y="195" data-speed="2400" data-start="800" data-easing="easeOutExpo">
					<img src="img/slider/clound.png" alt="" />
				</div>
					
				<!-- Caption -->
				<div class="tp-caption lfb" data-x="870" data-y="140" data-speed="1400" data-start="1800" data-easing="easeOutExpo">
					<img src="img/slider/rocket.png" alt="" />
				</div>
				
				<!-- Caption -->
				<div class="tp-caption lfb" data-x="825" data-y="330" data-speed="1500" data-start="1900" data-easing="easeOutExpo">
					<img src="img/slider/flames.png" alt="" />
				</div>
				
				<!-- Caption -->	
				<div class="caption sft stl" data-x="center" data-y="105" data-speed="1000" data-start="700" data-easing="easeOutExpo">
					<a href="Article_View.aspx?ID=10"><h3 class="rev-title bold">混合云平台</h3></a>
				</div>
				
				
                <!-- Caption -->
				<div class="caption lfl stl rev-title-sub" data-x="center" data-y="240" data-speed="800" data-start="1100" data-easing="easeOutExpo">
				再也不用担心数据不同步啦
				</div>
				
				<!-- Caption -->
				<div class="caption sfb" data-x="center" data-y="320" data-speed="1100" data-start="1500" data-easing="easeOutExpo">
					<a href="Article_View.aspx?ID=10" class="btn btn-primary btn-custom">更多</a>                 
				</div>
			</li>
			
			<!-- Slide 2 -->
				<li data-transition="slideleft">
				<img src="img/slider/slider2.jpg" alt="" />
				
				<!-- Caption -->
				<div class="tp-caption lfl" data-x="right" data-y="35" data-speed="1000" data-start="800" data-easing="easeOutExpo">
					<img src="img/slider/iMac.png" alt="" />
				</div>
					
				<!-- Caption -->
				<div class="caption lfl stl bg" data-x="20" data-y="60" data-speed="800" data-start="700" data-easing="easeOutExpo">
					<a href="Article_View.aspx?ID=3"><h2 class="rev-title big white">全媒体平台<br />成熟的网站群框架</h2></a>
				</div>
					
				<!-- Caption -->
				<div class="caption lfl stl rev-text rev-left" data-x="left" data-y="190" data-speed="800" data-start="1100" data-easing="easeOutExpo">
					<p class="hidden-phone">禾美全终端媒体平台，创新性地推出全终端媒体平台，<br />
					支持手机、平板、电脑、电视、微信、APP六大终端媒体，<br/>
					成为行业内第一个覆盖全终端的媒体管理和发布平台。
					</p>
				</div>
					
				<!-- Caption -->
				<div class="caption sfb stb rev-left" data-x="left" data-y="380" data-speed="1100" data-start="1500" data-easing="easeOutExpo">
					<a href="Article_View.aspx?ID=3" class="btn btn-inverse btn-custom mobile">具体详情</a>   &nbsp;&nbsp;
					<a href="#" class="btn btn-inverse btn-custom mobile">联系我们</a>                     
				</div>
			</li>
			
			<!-- Slide 3 -->
				<li data-transition="slideleft" >
				<img src="img/slider/slider3.jpg" alt="" />
				
				<!-- Caption -->	
				<div class="tp-caption lfl" data-x="right" data-y="35" data-speed="1000" data-start="800" data-easing="easeOutExpo">
					<img src="img/slider/wchat.png" alt="" />
				</div>
				
				<!-- Caption -->
				<div class="caption lfl stl bold bg rev-left" data-x="left" data-y="80" data-speed="800" data-start="1500" data-easing="easeOutExpo">
					<a href="Article_Preview.aspx?ID=11"><h3 class="rev-title big bold">校园微信预约系统</h3></a>
				</div>

				<!-- Caption -->
				<div class="caption lfl stl rev-text rev-left" data-x="left" data-y="200" data-speed="800" data-start="1700" data-easing="easeOutExpo">
					<p class="white hidden-phone">校内的讲座订票，心理咨询，上课签到，大型活动预报名，医务室预约看病等等，<br />
	都可以通过微信预约系统进行提前预约，从而省去繁琐的预约流程，节省时间<br />
			禾美公司立志给你提供最贴心的校园微信预约服务。</p>
				</div>
			</li>
			
			<!-- Slide 4 -->
				<li data-transition="slidedown">
				<img src="img/slider/slider4.jpg" alt="" />
				
				<!-- Caption -->
				<div class="caption fade fullscreenvideo" data-autoplay="false"  data-speed="500" data-start="500" data-easing="easeOutBack">
				 <a href="Article_View.aspx?ID=9"><img src="img/slider/slidera.jpg" alt="" /></a>
				</div>
				
				<div class="caption big_white sft stt" data-x="center" data-y="25" data-speed="300" data-start="500" data-easing="easeOutExpo" data-end="4000" 
				data-endspeed="300" data-endeasing="easeInSine">
				 <a href="Article_View.aspx?ID=9">资源库管理系统</a>
				</div>
			</li>
		</ul>
			<div class="tp-bannertimer tp-bottom"></div>
            </div>
        </div>
       <!-- // END REVOLUTION SLIDER -->
    <div class="container wrapper">
	<div class="inner_content">
	<div class="pad45"></div>
	
	<!--info boxes-->
	<div class="row">
		<div class="span3">
			<div class="tile">
			<div class="intro-icon-disc cont-large"><i class="fa fa-wrench intro-icon-large"></i></div>
			<h6><small>DESIGN</small>
			<br /><a href="Article_View.aspx?ID=3"><span>全媒体平台</span></a></h6>
			<p>禾美全终端媒体平台，创新性地推出全终端媒体平台，支持手机、平板、电脑、电视、微信、APP六大终端媒体，成为行业内第一个覆盖全终端的媒体管理和发布平台。  </p>
			</div> 
				<div class="pad25"></div>
				</div> 
				
			<div class="span3">
				<div class="tile">
				<div class="intro-icon-disc cont-large"><i class="fa fa-rocket intro-icon-large"></i></div>
				<h6><small>CODE</small>
				<br /><a href="Article_View.aspx?ID=6"><span>网站群管理系统</span></a></h6>
				<p>禾美网站群管理系统，以集中部署、统一验证、分级管理、资源共享为核心，帮助政府、企业和高校将一群信息孤立网站，转变为风格统一、运营规范的整体网站。</p>
				</div> 
					<div class="pad25"></div>
					</div> 
					
			<div class="span3">
				<div class="tile">
				<div class="intro-icon-disc cont-large"><i class="fa fa-flask intro-icon-large"></i></div>
				<h6><small>CREATE</small>
				<br /><a href="Article_View.aspx?ID=7"><span>微信网站系统</span></a></h6>
				<p>禾美微信网站系统，把企业的门户网站与微信公众号实现无缝整合，给企业和组织提供强大的业务服务能力和用户管理能力，帮助企业快速实现微信公众号的有效运营和管理。 </p>	
				</div> 
					<div class="pad25"></div>
					</div> 
				
			<div class="span3">
				<div class="tile ">
				<div class="intro-icon-disc cont-large"><i class="fa fa-book  intro-icon-large"></i></div>
				<h6> <small>SUPPORT</small>
				<br /><a href="Article_View.aspx?ID=8"><span>手机网站系统</span></a></h6>
				<p>禾美手机网站系统，以手机用户的浏览体验为核心，精心设计手机页面，优化响应时间，提高用户满意度和转化率。锐赛思提供了手机网站的设计、开发、管理、和运维的一站式解决方案。</p>
				</div>
					<div class="pad25"></div>	
				</div> 
				</div> 
				
			<!--//info boxes-->
			<div class="row">
			<!--col 1-->
			<div class="span12">
			<div class="row">
			<div class="pad25 hidden-phone"></div>	
			
			<div class="span4">
			<h1>成功案例</h1>
			<h4>本公司从事软件开发多年，开发实力强劲，主要从事网站设计、制作，网站优化、推广等服务。本公司同时拥有一大批的互联网骨干精英，为广大的客户提供专业贴心的服务。</h4>
			<p>
            </p>
			
			<a href="#" class="btn btn-primary  btn-custom btn-rounded">更多案例</a>
			<div class="pad45"></div>
			</div>
			<!--column 2 slider-->
			<div class="span8 pad15 col_full2">
			
			<div id="slider_home">
            <div class="slider-item">	
				<div class="slider-image">
				<div class="hover_colour">
			<a href="img/large/1.gif" data-rel="prettyPhoto">
					<img src="img/small/1.gif" alt="" /></a>
					</div>
				</div>
				<div class="slider-title">
				<h3><a href="Article_View.aspx?ID=5">教育技术与传播学院官网</a></h3>
				<p>独立为广东省实力强劲的高校广东技术师范学院的二级学院教育技术与传播学院独立定制的网站系统，得到该学院的广大师生的好评</p>
				</div>
			</div>
			
			<div class="slider-item">
				<div class="slider-image">
				<div class="hover_colour">
				<a href="img/large/3.gif" data-rel="prettyPhoto">
					<img src="img/small/3.gif" alt="" /></a>
					</div>
				</div>
				<div class="slider-title">
				<h3><a href="Article_View.aspx?ID=12">顺德研究院官网</a></h3>
				<p>工业4.0，德国工业专家和中国学者共聚顺德，指点江山，激扬文字，商讨工业的新时代</p>
				</div>
			</div>
			
			<div class="slider-item">
				<div class="slider-image">
				<div class="hover_colour">
				<a href="img/large/2.gif" data-rel="prettyPhoto">
					<img src="img/small/2.gif" alt="" /></a>
					</div>
				</div>
				<div class="slider-title">
				<h3><a href="Article_View.aspx?ID=13">多维度精神康复系统</a></h3>
				<p>专业化，量身为现代大中小型医院定制的医院信息管理系统，智能化，便捷化，信息化，减轻医院的信息负担，节省医疗成本</p>
				</div>
			</div>
			
			<div class="slider-item">
				<div class="slider-image">
				<div class="hover_colour">
				<a href="img/large/s4.jpg" data-rel="prettyPhoto">
					<img src="img/small/s4.jpg" alt="" /></a>
					</div>
				</div>
				<div class="slider-title">
				<h3><a href="#">infographics</a></h3>
				<p>An his tamquam postulant, pri id mazim nostrud diceret.</p>
				</div>
			</div>
			
			
			
				</div>
				<div id="sl-prev" class="widget-scroll-prev"><i class="fa fa-chevron-left white"></i></div>
				<div id="sl-next" class="widget-scroll-next"><i class="fa fa-chevron-right white but_marg"></i></div>
			</div>
				</div>
				</div>
			</div>
		</div>
		<!--//page-->
	</div>
</asp:Content>

