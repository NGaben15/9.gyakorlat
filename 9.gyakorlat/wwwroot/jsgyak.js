console.log("666")

var pascal = document.getElementById("pascal");


var faktorialis = function (n) {
    let er = 1;
    for (let i = 2; i <=n; i++) {
        er = er * i;
    }
    return er;
}

var faktorialisR = (n) => {
    if (n === 0 || n === 1) {
        return 1;
    } else {
        return n * faktorialisR(n - 1);
    }
}

function szamitas() {
    let n = document.getElementById("nTb").value;
    let n2 = parseInt(n);
    if (n2) {
        let er = faktorialis(n);
        document.getElementById("eredményDiv").innetText = er;
    }
    else {
        document.getElementById("eredményDiv").innetText = "?";
    }
    
    
    alert(n)
    
}
