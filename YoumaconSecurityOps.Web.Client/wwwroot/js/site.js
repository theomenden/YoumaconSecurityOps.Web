// play on load for gallery view
setTimeout(transition, 1000);

document.querySelector('.js-trigger-transition').addEventListener('click', function (e) {
    e.preventDefault();
    transition();
});

function transition() {
    const tl = new TimelineMax();

    tl.to(CSSRulePlugin.getRule('body:before'), 0.2, { cssRule: { top: '50%' }, ease: Power2.easeOut }, 'close')
        .to(CSSRulePlugin.getRule('body:after'), 0.2, { cssRule: { bottom: '50%' }, ease: Power2.easeOut }, 'close')
        .to(document.querySelector('.loader'), 0.2, { opacity: 1 })
        .to(CSSRulePlugin.getRule('body:before'), 0.2, { cssRule: { top: '0%' }, ease: Power2.easeOut }, '+=1.5', 'open')
        .to(CSSRulePlugin.getRule('body:after'), 0.2, { cssRule: { bottom: '0%' }, ease: Power2.easeOut }, '-=0.2', 'open')
        .to(document.querySelector('.loader'), 0.2, { opacity: 0 }, '-=0.2');
}