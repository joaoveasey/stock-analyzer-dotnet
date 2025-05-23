window.applyMoneyMask = (element) => {
    IMask(element, {
        mask: 'R$ num',
        blocks: {
            num: {
                mask: Number,
                thousandsSeparator: '.',
                radix: ',',
                scale: 2,
                signed: false
            }
        }
    });
};

window.applyPercentMask = (element) => {
    IMask(element, {
        mask: 'num%',
        blocks: {
            num: {
                mask: Number,
                scale: 2,
                signed: false,
                radix: ',',
                thousandsSeparator: '.'
            }
        }
    });
};
