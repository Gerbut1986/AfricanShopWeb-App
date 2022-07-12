$(function(){
    $('a[href^="#"]').click(function (){
        var elementClick = $(this).attr("href");
        var destination = $(elementClick).offset().top;
        jQuery("html:not(:animated), body:not(:animated)").animate({scrollTop: destination}, 800);
        return false;
    });
    
    times = function(){         
        now = new Date();  
        hour = 24-now.getHours();
        hour = ((hour+'').length==1?hour='0'+hour:hour)+'';
        minu = 60-now.getMinutes();
        minu = ((minu+'').length==1?minu='0'+minu:minu)+'';
        secu = 60-now.getSeconds(); 
        secu = ((secu+'').length==1?secu='0'+secu:secu)+'';
        $('.timer-action').html('<span class="timer-col"><strong>'+hour+'</strong><br/><span>часов</span></span><span class="timer-col"><strong>'+minu+'</strong><br/><span>минут</span></span><span class="timer-col"><strong>'+secu+'</strong><br/><span>секунд</span></span>');
    }   
    times();                                              
    setInterval(times,1000);
    
    $('.slider-cont').slick({
      infinite: true,
      autoplay: true,
      autoplaySpeed: 3000,
      adaptiveHeight: false,
      dots: false,
      arrows: true,
      fade: false,
      speed: 300,
      slidesToShow: 1,
      slidesToScroll: 1,
      prevArrow: '<span data-role="none" class="slick-prev animate" aria-label="Previous" tabindex="0" role="button"></span>',
      nextArrow: '<span data-role="none" class="slick-next animate" aria-label="Next" tabindex="0" role="button"></span>'
    });  

});

function CountBox() {
    dateNow = new Date();
    amount = ((23 - dateNow.getHours())*60*60 + (59 - dateNow.getMinutes())*60 + (60 - dateNow.getSeconds()))*1000;
    delete dateNow;
    if (amount < 0) {
        out = "<div class='countbox__num'><div class='countbox__num-hours1'><span></span>0</div><div class='countbox__num-hours2'><span></span>0</div><div class='countbox__num-text'></div></div>" +
            "<div class='countbox__space'></div>" +
            "<div class='countbox__num'><div class='countbox__num-mins1'><span></span>0</div><div class='countbox__num-mins2'><span></span>0</div><div class='countbox__num-text'></div></div>" +
            "<div class='countbox__space'></div>" +
            "<div class='countbox__num'><div class='countbox__num-secs1'><span></span>0</div><div class='ccountbox__num-secs2'><span></span>0</div><div class='countbox__num-text'></div></div>";
        var list = document.getElementsByClassName("countbox");
        for (var i = 0; i < list.length; i++) {
            list[i].innerHTML = out;
        }
        setTimeout("CountBox()", 10000)
    } else {
        days = 0;
        days1 = 0;
        days2 = 0;
        hours = 0;
        hours1 = 0;
        hours2 = 0;
        mins = 0;
        mins1 = 0;
        mins2 = 0;
        secs = 0;
        secs1 = 0;
        secs2 = 0;
        out = "";
        amount = Math.floor(amount / 1e3);
        days = Math.floor(amount / 86400);
        days1 = (days >= 10) ? days.toString().charAt(0) : '0';
        days2 = (days >= 10) ? days.toString().charAt(1) : days.toString().charAt(0);
        amount = amount % 86400;
        hours = Math.floor(amount / 3600);
        hours1 = (hours >= 10) ? hours.toString().charAt(0) : '0';
        hours2 = (hours >= 10) ? hours.toString().charAt(1) : hours.toString().charAt(0);
        amount = amount % 3600;
        mins = Math.floor(amount / 60);
        mins1 = (mins >= 10) ? mins.toString().charAt(0) : '0';
        mins2 = (mins >= 10) ? mins.toString().charAt(1) : mins.toString().charAt(0);
        amount = amount % 60;
        secs = Math.floor(amount);
        secs1 = (secs >= 10) ? secs.toString().charAt(0) : '0';
        secs2 = (secs >= 10) ? secs.toString().charAt(1) : secs.toString().charAt(0);
        out = "<div class='countbox__num'><div class='round-border'><div class='countbox__num-hours1'><span></span>" + hours1 + "</div><div class='countbox__num-hours2'><span></span>" + hours2 + "</div></div><div class='countbox__num-text'>часа</div></div>" +
            "<div class='countbox__space'></div>" +
            "<div class='countbox__num'><div class='round-border'><div class='countbox__num-mins1'><span></span>" + mins1 + "</div><div class='countbox__num-mins2'><span></span>" + mins2 + "</div></div><div class='countbox__num-text'>минути</div></div>" +
            "<div class='countbox__space'></div>" +
            "<div class='countbox__num'><div class='round-border'><div class='countbox__num-secs1'><span></span>" + secs1 + "</div><div class='countbox__num-secs2'><span></span>" + secs2 + "</div></div><div class='countbox__num-text'>секунди</div></div>";
        var list = document.getElementsByClassName("countbox");
        for (var i = 0; i < list.length; i++) {
            list[i].innerHTML = out;
        }
        setTimeout("CountBox()", 1e3)
    }
}
CountBox();
 