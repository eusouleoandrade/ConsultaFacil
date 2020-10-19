function setLoadBox(nameIdBox, nameClassBox = null) {

    if (nameClassBox == null) {
        nameClassBox = '.ibox-content';
    };

    $(nameIdBox).children(nameClassBox).toggleClass('sk-loading');
}