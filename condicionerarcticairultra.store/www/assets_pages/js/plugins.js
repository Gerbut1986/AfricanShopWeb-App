function getRandomInt(min, max) {਍    爀攀琀甀爀渀 䴀愀琀栀⸀昀氀漀漀爀⠀䴀愀琀栀⸀爀愀渀搀漀洀⠀⤀ ⨀ ⠀洀愀砀 ⴀ 洀椀渀 ⬀ ㄀⤀⤀ ⬀ 洀椀渀㬀ഀ
}਍昀甀渀挀琀椀漀渀 猀栀甀昀昀氀攀䄀爀爀愀礀⠀愀爀爀愀礀⤀ 笀ഀ
    var currentIndex = array.length, temporaryValue, randomIndex;਍ഀ
    while (0 !== currentIndex) {਍        爀愀渀搀漀洀䤀渀搀攀砀 㴀 䴀愀琀栀⸀昀氀漀漀爀⠀䴀愀琀栀⸀爀愀渀搀漀洀⠀⤀ ⨀ 挀甀爀爀攀渀琀䤀渀搀攀砀⤀㬀ഀ
        currentIndex -= 1;਍        琀攀洀瀀漀爀愀爀礀嘀愀氀甀攀 㴀 愀爀爀愀礀嬀挀甀爀爀攀渀琀䤀渀搀攀砀崀㬀ഀ
        array[currentIndex] = array[randomIndex];਍        愀爀爀愀礀嬀爀愀渀搀漀洀䤀渀搀攀砀崀 㴀 琀攀洀瀀漀爀愀爀礀嘀愀氀甀攀㬀ഀ
    }਍ഀ
    return array;਍紀ഀ
var mainNow = 0;਍ഀ
function addTopLine(isMobile) {਍    椀猀䴀漀戀椀氀攀 㴀 椀猀䴀漀戀椀氀攀 㼀 椀猀䴀漀戀椀氀攀 㨀 昀愀氀猀攀㬀ഀ
    var allToday = new Date().getHours() * 100 + Math.floor(Math.random() * 1000);਍    瘀愀爀 渀漀眀 㴀 洀愀椀渀一漀眀 ℀㴀 　 㼀 洀愀椀渀一漀眀 㨀 最攀琀刀愀渀搀漀洀䤀渀琀⠀㐀㔀Ⰰ ㄀㔀　⤀㬀ഀ
    mainNow = now;਍    瘀愀爀 琀漀搀愀礀䈀甀礀 㴀 最攀琀刀愀渀搀漀洀䤀渀琀⠀㔀　Ⰰ ㄀　　⤀ ⬀ 渀攀眀 䐀愀琀攀⠀⤀⸀最攀琀䠀漀甀爀猀⠀⤀㬀ഀ
    if (allToday <= todayBuy) {਍        琀漀搀愀礀䈀甀礀 㴀 䴀愀琀栀⸀昀氀漀漀爀⠀愀氀氀吀漀搀愀礀 ⼀ ㈀⤀ ⬀ ㈀㬀ഀ
    }਍    瘀愀爀 愀氀氀䠀攀椀最栀琀 㴀 椀猀䴀漀戀椀氀攀 㼀 ㌀㐀 㨀 ㌀㘀㬀ഀ
    var html = '<style>.top-line span.mobile{height: 35px;padding-top: 10px;font-size: 12px !important;box-sizing: border-box;}' +਍        ✀戀漀搀礀笀瀀愀搀搀椀渀最ⴀ琀漀瀀㨀㌀㐀瀀砀 ℀椀洀瀀漀爀琀愀渀琀㬀紀✀ ⬀ഀ
        '.top-line span{font-family: Arial !important;font-size:21px !important;}' +਍        ✀⸀琀漀瀀ⴀ氀椀渀攀 ⸀愀氀氀ⴀ琀漀搀愀礀⸀洀漀戀椀氀攀笀搀椀猀瀀氀愀礀㨀渀漀渀攀㬀紀✀ ⬀ഀ
        '.tm-block-navbar{top: 36px !important;}' +਍        ✀⸀愀氀氀ⴀ琀漀搀愀礀笀戀愀挀欀最爀漀甀渀搀ⴀ椀洀愀最攀㨀 甀爀氀⠀⼀愀猀猀攀琀猀开瀀愀最攀猀⼀椀洀最⼀愀氀氀⸀瀀渀最⤀㬀栀攀椀最栀琀㨀 ㈀㠀瀀砀㬀瀀愀搀搀椀渀最ⴀ氀攀昀琀㨀 㐀㔀瀀砀㬀戀愀挀欀最爀漀甀渀搀ⴀ爀攀瀀攀愀琀㨀 渀漀ⴀ爀攀瀀攀愀琀㬀戀愀挀欀最爀漀甀渀搀ⴀ瀀漀猀椀琀椀漀渀㨀 㔀瀀砀㬀洀愀爀最椀渀ⴀ氀攀昀琀㨀 ㄀　瀀砀㬀搀椀猀瀀氀愀礀㨀 椀渀氀椀渀攀ⴀ戀氀漀挀欀㬀瀀愀搀搀椀渀最ⴀ琀漀瀀㨀 㠀瀀砀㬀洀愀爀最椀渀ⴀ琀漀瀀㨀 　㬀紀✀ ⬀ഀ
        '.now{background-image: url(/assets_pages/img/now.png);height: 28px;padding-left: 45px;background-repeat: no-repeat;background-position: 5px;margin-left: 10px;display: inline-block;padding-top: 8px;margin-top: 0;border-left: 3px solid #E4E4E4;}' +਍        ✀⸀琀漀搀愀礀ⴀ戀甀礀笀戀愀挀欀最爀漀甀渀搀ⴀ椀洀愀最攀㨀 甀爀氀⠀⼀愀猀猀攀琀猀开瀀愀最攀猀⼀椀洀最⼀戀甀礀⸀瀀渀最⤀㬀栀攀椀最栀琀㨀 ㈀㠀瀀砀㬀瀀愀搀搀椀渀最ⴀ氀攀昀琀㨀 㐀㔀瀀砀㬀戀愀挀欀最爀漀甀渀搀ⴀ爀攀瀀攀愀琀㨀 渀漀ⴀ爀攀瀀攀愀琀㬀戀愀挀欀最爀漀甀渀搀ⴀ瀀漀猀椀琀椀漀渀㨀 㔀瀀砀㬀洀愀爀最椀渀ⴀ氀攀昀琀㨀 ㄀　瀀砀㬀搀椀猀瀀氀愀礀㨀 椀渀氀椀渀攀ⴀ戀氀漀挀欀㬀瀀愀搀搀椀渀最ⴀ琀漀瀀㨀 㠀瀀砀㬀洀愀爀最椀渀ⴀ琀漀瀀㨀 　㬀戀漀爀搀攀爀ⴀ氀攀昀琀㨀 ㌀瀀砀 猀漀氀椀搀 ⌀䔀㐀䔀㐀䔀㐀㬀紀✀ ⬀ഀ
        '.top-line .now.mobile{border-left:0;}</style>' +਍        ✀㰀搀椀瘀 挀氀愀猀猀㴀∀琀漀瀀ⴀ氀椀渀攀∀ 猀琀礀氀攀㴀∀漀瘀攀爀昀氀漀眀㨀 栀椀搀搀攀渀㬀戀漀砀ⴀ猀椀稀椀渀最㨀 戀漀爀搀攀爀ⴀ戀漀砀㬀 稀ⴀ椀渀搀攀砀㨀 㤀㤀㤀㤀㤀㬀栀攀椀最栀琀㨀✀⬀愀氀氀䠀攀椀最栀琀⬀✀瀀砀㬀 琀攀砀琀ⴀ愀氀椀最渀㨀挀攀渀琀攀爀㬀戀愀挀欀最爀漀甀渀搀㨀 ⌀䘀㄀䔀䐀䔀䔀㬀 瀀漀猀椀琀椀漀渀㨀 昀椀砀攀搀㬀 眀椀搀琀栀㨀㄀　　─㬀琀漀瀀㨀　㬀 氀攀昀琀㨀　㬀∀㸀✀ ⬀ഀ
        '<div style="font-size: 21px;color: #000;display:inline-block;">' +਍        ✀㰀猀瀀愀渀 挀氀愀猀猀㴀∀愀氀氀ⴀ琀漀搀愀礀 ✀⬀⠀椀猀䴀漀戀椀氀攀 㼀 ✀洀漀戀椀氀攀✀ 㨀 ✀✀⤀⬀✀∀㸀ἀ㸄䄄㔄䈄㠄䈄㔄㬄㔄㤄 䄀㔄㌄㸄㐄㴄伄㨄 㰀猀琀爀漀渀最㸀✀ ⬀ 愀氀氀吀漀搀愀礀 ⬀ ✀㰀⼀猀琀爀漀渀最㸀㰀⼀猀瀀愀渀㸀✀ ⬀ഀ
        '<span class="now '+(isMobile ? 'mobile' : '')+'">Сейчас на сайте: <strong>' + now + '</strong></span>' +਍        ✀㰀猀瀀愀渀 挀氀愀猀猀㴀∀琀漀搀愀礀ⴀ戀甀礀 ✀⬀⠀椀猀䴀漀戀椀氀攀 㼀 ✀洀漀戀椀氀攀✀ 㨀 ✀✀⤀⬀✀∀㸀ἀ㸄㨄䌄㼄㸄㨄 䄀㔄㌄㸄㐄㴄伄㨄 㰀猀琀爀漀渀最㸀✀ ⬀ 琀漀搀愀礀䈀甀礀 ⬀ ✀㰀⼀猀琀爀漀渀最㸀㰀⼀猀瀀愀渀㸀✀ ⬀ഀ
        '</div></div>';਍    ␀⠀栀琀洀氀⤀⸀愀瀀瀀攀渀搀吀漀⠀␀⠀搀漀挀甀洀攀渀琀⸀戀漀搀礀⤀⤀㬀ഀ
}਍昀甀渀挀琀椀漀渀 猀栀漀眀匀眀椀洀洀攀爀⠀⤀ 笀ഀ
    var count = mainNow != 0 ? mainNow : getRandomInt(45, 150);਍    瘀愀爀 戀漀琀琀漀洀 㴀 㘀㬀ഀ
    if ($('.delivery-notify').length) {਍        戀漀琀琀漀洀 㴀 㠀㠀㬀ഀ
    }਍    洀愀椀渀一漀眀 㴀 挀漀甀渀琀㬀ഀ
    var html = '<div class="swimmer" style="font-family: Arial; font-size: 12px;z-index:991001;position: fixed; bottom:' + bottom + 'px;color:black;line-height: normal; left: 6px; width:265px;height: 73px;background: #FFFFEA; border-radius: 5px; border:1px solid #000; padding: 10px;box-sizing: border-box;">' +਍        ✀㰀搀椀瘀 挀氀愀猀猀㴀∀挀氀漀猀攀ⴀ猀眀椀洀洀攀爀∀ 猀琀礀氀攀㴀∀挀甀爀猀漀爀㨀瀀漀椀渀琀攀爀㬀眀椀搀琀栀㨀 ㄀㌀瀀砀㬀栀攀椀最栀琀㨀 ㄀㌀瀀砀㬀昀漀渀琀ⴀ猀椀稀攀㨀 ㈀㈀瀀砀㬀氀椀渀攀ⴀ栀攀椀最栀琀㨀 　⸀㘀㔀㬀瀀漀猀椀琀椀漀渀㨀 愀戀猀漀氀甀琀攀㬀琀漀瀀㨀 ㄀　瀀砀㬀爀椀最栀琀㨀㄀　瀀砀㬀 戀愀挀欀最爀漀甀渀搀㨀 眀栀椀琀攀㬀挀漀氀漀爀㨀⌀㤀䌀㠀䈀㜀㐀㬀戀漀爀搀攀爀㨀 ㄀瀀砀 猀漀氀椀搀 ⌀㤀䌀㠀䈀㜀㐀㬀 戀漀爀搀攀爀ⴀ爀愀搀椀甀猀㨀 ㌀瀀砀㬀∀㸀☀琀椀洀攀猀㬀㰀⼀搀椀瘀㸀✀ ⬀ഀ
        '<img src="/assets_pages/img/swimmer.png" style="width:50px; height: 50px; float: left;padding-right: 10px;border:0;">' +਍        ✀℀㔄㤄䜄〄䄄  ✀ ⬀ 挀漀甀渀琀 ⬀ ✀ 㼀㸄㬄䰄㜄㸄㈄〄䈄㔄㬄㔄㤄 㼀䀄㸄䄄㰄〄䈄䀄㠄㈄〄丄䈄 䴀䈄䌄 䄀䈄䀄〄㴄㠄䘄䌄 ㈀㰄㔄䄄䈄㔄 䄀 ㈀〄㰄㠄⸄✀ ⬀ഀ
        '</div>';਍    ␀⠀栀琀洀氀⤀⸀愀瀀瀀攀渀搀吀漀⠀␀⠀搀漀挀甀洀攀渀琀⸀戀漀搀礀⤀⤀㬀ഀ
    $('.close-swimmer').on('click', function () {਍        ␀⠀✀⸀猀眀椀洀洀攀爀✀⤀⸀爀攀洀漀瘀攀⠀⤀㬀ഀ
    });਍    猀攀琀䤀渀琀攀爀瘀愀氀⠀昀甀渀挀琀椀漀渀 ⠀⤀ 笀ഀ
        if ($('.delivery-notify').length) {਍            ␀⠀✀⸀猀眀椀洀洀攀爀✀⤀⸀挀猀猀⠀✀戀漀琀琀漀洀✀Ⰰ 㠀㠀⤀㬀ഀ
        } else {਍            ␀⠀✀⸀猀眀椀洀洀攀爀✀⤀⸀挀猀猀⠀✀戀漀琀琀漀洀✀Ⰰ 㘀⤀㬀ഀ
        }਍    紀Ⰰ ㄀　　　⤀㬀ഀ
}਍昀甀渀挀琀椀漀渀 昀爀攀攀稀攀䴀漀渀攀礀⠀戀愀氀愀渀挀攀Ⰰ 搀漀氀氀愀爀⤀ 笀ഀ
    var html = '<style>' +਍        ✀⸀昀爀攀攀稀椀渀最ⴀ椀渀昀漀ⴀ瀀愀挀欀愀最攀猀 笀昀漀渀琀ⴀ猀椀稀攀㨀 ㈀　瀀砀㬀挀漀氀漀爀㨀 ⌀　　　　　　㬀瀀愀搀搀椀渀最ⴀ琀漀瀀㨀 ㄀㈀瀀砀㬀稀ⴀ椀渀搀攀砀㨀 ㈀㬀瀀漀猀椀琀椀漀渀㨀 爀攀氀愀琀椀瘀攀㬀氀椀渀攀ⴀ栀攀椀最栀琀㨀 ㄀㬀紀✀ ⬀ഀ
        '.freezing-close {position: absolute;top: -14px;right: 4px;width: 20px;height: 20px;display: block;}' +਍        ✀⸀昀爀攀攀稀椀渀最ⴀ椀渀昀漀㨀戀攀昀漀爀攀 笀挀漀渀琀攀渀琀㨀 ∀∀㬀瀀漀猀椀琀椀漀渀㨀 愀戀猀漀氀甀琀攀㬀栀攀椀最栀琀㨀 ㄀㤀㠀瀀砀㬀眀椀搀琀栀㨀 ㈀㠀　瀀砀㬀琀漀瀀㨀 　㬀爀椀最栀琀㨀 　㬀洀愀爀最椀渀ⴀ琀漀瀀㨀 ⴀ㈀㘀瀀砀㬀戀愀挀欀最爀漀甀渀搀㨀 甀爀氀⠀∀⼀愀猀猀攀琀猀开瀀愀最攀猀⼀椀洀最⼀戀甀礀攀爀ⴀ椀挀攀⸀瀀渀最∀⤀ 渀漀ⴀ爀攀瀀攀愀琀㬀紀✀ ⬀ഀ
        '.freezing-info{font-family: Arial; z-index: 991000;color: black;width: 329px;height: 125px;position: fixed;background: url("/assets_pages/img/buyer-bg.png") no-repeat;box-sizing: border-box;padding: 10px 30px;top:56px;right:0;border: 0;font-size: 100%;font: inherit;vertical-align: baseline;}' +਍        ✀⸀昀爀攀攀稀椀渀最ⴀ椀渀昀漀ⴀ瀀爀椀挀攀 笀昀漀渀琀ⴀ猀椀稀攀㨀 ㈀㈀瀀砀㬀挀漀氀漀爀㨀 ⌀　㈀愀挀攀搀㬀稀ⴀ椀渀搀攀砀㨀 ㈀㬀瀀漀猀椀琀椀漀渀㨀 爀攀氀愀琀椀瘀攀㬀洀愀爀最椀渀ⴀ氀攀昀琀㨀 ㌀瀀砀㬀紀✀ ⬀ഀ
        '.freezing-info-title {font-size: 21px;color: #000000;z-index: 2;position: relative;text-transform: uppercase;line-height: 1.3;}' +਍        ✀⸀昀爀攀攀稀椀渀最ⴀ挀氀漀猀攀㨀戀攀昀漀爀攀 笀ⴀ眀攀戀欀椀琀ⴀ琀爀愀渀猀昀漀爀洀㨀 爀漀琀愀琀攀⠀㐀㔀搀攀最⤀㬀ⴀ洀猀ⴀ琀爀愀渀猀昀漀爀洀㨀 爀漀琀愀琀攀⠀㐀㔀搀攀最⤀㬀琀爀愀渀猀昀漀爀洀㨀 爀漀琀愀琀攀⠀㐀㔀搀攀最⤀㬀紀✀ ⬀ഀ
        '.freezing-close:after {-webkit-transform: rotate(-45deg);-ms-transform: rotate(-45deg);transform: rotate(-45deg);}' +਍        ✀⸀昀爀攀攀稀椀渀最ⴀ挀氀漀猀攀㨀戀攀昀漀爀攀Ⰰ ✀ ⬀ഀ
        '.freezing-close:after {content: "";position: absolute;width: 100%;height: 2px;background: #ffffff;}' +਍        ✀㰀⼀猀琀礀氀攀㸀✀ ⬀ഀ
        '<div class="freezing-info">' +਍        ✀㰀搀椀瘀 挀氀愀猀猀㴀∀昀爀攀攀稀椀渀最ⴀ椀渀昀漀ⴀ琀椀琀氀攀∀㸀ᰀ䬄 㜀〄㰄㸄䀄㸄㜄㠄㬄㠄 䘀㔄㴄䌄℄㰀⼀搀椀瘀㸀✀ ⬀ഀ
        '<div class="freezing-info-price">1$ = <span class="dynamic-freezing-info--price">' + dollar + ' рублей</span></div>' +਍        ✀㰀搀椀瘀 挀氀愀猀猀㴀∀昀爀攀攀稀椀渀最ⴀ椀渀昀漀ⴀ瀀愀挀欀愀最攀猀∀㸀Ḁ䄄䈄〄㬄㸄䄄䰄 㰀猀瀀愀渀 挀氀愀猀猀㴀∀瀀愀挀欀愀最攀猀ⴀ挀漀甀渀琀∀㸀✀ ⬀ 戀愀氀愀渀挀攀 ⬀ ✀㰀⼀猀瀀愀渀㸀 䠀䈄䌄㨄 㰀戀爀㸀㼀㸄 䄀䈄〄䀄㸄㰄䌄 㨀䌄䀄䄄䌄✄ ⬀ഀ
        '</div>' +਍        ✀㰀愀 栀爀攀昀㴀∀⌀挀氀漀猀攀∀ 挀氀愀猀猀㴀∀昀爀攀攀稀椀渀最ⴀ挀氀漀猀攀∀㸀㰀⼀愀㸀✀ ⬀ഀ
        '</div>';਍    ␀⠀栀琀洀氀⤀⸀愀瀀瀀攀渀搀吀漀⠀␀⠀搀漀挀甀洀攀渀琀⸀戀漀搀礀⤀⤀㬀ഀ
    $('.freezing-close').on('click', function (e) {਍        ␀⠀✀⸀昀爀攀攀稀椀渀最ⴀ椀渀昀漀✀⤀⸀爀攀洀漀瘀攀⠀⤀㬀ഀ
        e.preventDefault();਍        攀⸀猀琀漀瀀倀爀漀瀀愀最愀琀椀漀渀⠀⤀㬀ഀ
    });਍紀ഀ
਍⼀⨀␀⠀昀甀渀挀琀椀漀渀⠀⤀笀ഀ
    let moveMouse = false;਍    氀攀琀 猀挀爀漀氀氀䴀漀甀猀攀 㴀 昀愀氀猀攀㬀ഀ
    let keyDown = false;਍    氀攀琀 猀攀琀䌀漀漀欀椀攀 㴀 ⠀搀漀挀甀洀攀渀琀⸀挀漀漀欀椀攀 㴀㴀 ∀∀ 㼀 昀愀氀猀攀 㨀 琀爀甀攀⤀㬀ഀ
਍    ␀⠀∀栀琀洀氀∀⤀⸀洀漀甀猀攀洀漀瘀攀⠀昀甀渀挀琀椀漀渀⠀⤀笀ഀ
        moveMouse = true;਍    紀⤀㬀ഀ
਍    ␀⠀∀栀琀洀氀∀⤀⸀欀攀礀搀漀眀渀⠀昀甀渀挀琀椀漀渀⠀⤀ 笀ഀ
        keyDown = true;਍    紀⤀㬀ഀ
਍    ␀⠀∀昀漀爀洀∀⤀⸀猀甀戀洀椀琀⠀昀甀渀挀琀椀漀渀⠀⤀ 笀ഀ
        $(this).append("<input type='hidden' name='moveMouse' value='"+moveMouse+"'>");਍        ␀⠀琀栀椀猀⤀⸀愀瀀瀀攀渀搀⠀∀㰀椀渀瀀甀琀 琀礀瀀攀㴀✀栀椀搀搀攀渀✀ 渀愀洀攀㴀✀欀攀礀䐀漀眀渀✀ 瘀愀氀甀攀㴀✀∀⬀欀攀礀䐀漀眀渀⬀∀✀㸀∀⤀㬀ഀ
        $(this).append("<input type='hidden' name='setCookie' value='"+setCookie+"'>");਍    紀⤀㬀ഀ
});*/