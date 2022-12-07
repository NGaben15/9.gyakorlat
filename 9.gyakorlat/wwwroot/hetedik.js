var hotList = [];           //Az éppen gyakoroltatott kérdések listája 
var questionsInHotList = 3; //Ez majd 7 lesz, teszteléshez jobb a 3.
var displayedQuestion;      //A hotList-ből éppen ez a kérdés van kint
var numberOfQuestions;      //Kérdések száma a teljes adatbázisban
var nextQuestion = 1;
var questionsInHotList = 3; //Ez majd 7 lesz, teszteléshez jobb a 3. 
var questionNumber = 1;
var destination=0;

function Kerdesmegjelenites() {
        let kerdes = hotList;
        console.log(kerdes);
        document.getElementById("kérdés_szöveg").innerText = kerdes.question1
        document.getElementById("válasz1").innerText = kerdes.answer1
        document.getElementById("válasz2").innerText = kerdes.answer2
        document.getElementById("válasz3").innerText = kerdes.answer3
        joValasz = kerdes.correctAnswer;
        if (kerdes.image) {
            document.getElementById("kép").src = "https://szoft1.comeback.hu/hajo/" + kerdes.image;
            document.getElementById("kép").classList.remove("rejtett")
        }
        else {
            document.getElementById("kép").classList.add("rejtett")
        }
        //Jó és rossz kérdések jelölésének levétele
        document.getElementById("válasz1").classList.remove("jó", "rossz");
        document.getElementById("válasz2").classList.remove("jó", "rossz");
        document.getElementById("válasz3").classList.remove("jó", "rossz");
    }

    function kerdesBetoltes(id) {
        fetch(`/questions/${questionNumber}`)
            .then(
                response => {
                    if (!response.ok) {
                        console.error(`Hibás letöltés: ${response.status}`)
                    }
                    else {
                        return response.json()
                    }
                }
            )
            .then(
                q => {
                    hotList = q;
                    console.log(`A ${questionNumber}. kérdés letöltve a hot list ${destination}. helyére`)
                    if (displayedQuestion == undefined && destination == 0) { //!!!!!!!!!!!!!
                        displayedQuestion = 0;
                        Kerdesmegjelenites();
                    }
                }
            );
    }

    function valaszFeldolgozas(valasz) {
        if (!valasz.ok) {
            console.error(`Hibás válasz: ${response.status}`)
        }
        else {
            return valasz.json()
        }
    }
    var joValasz;
    var questionId = 4

    function elore() {
        displayedQuestion++;
        if (displayedQuestion == questionsInHotList) displayedQuestion = 0;
        Kerdesmegjelenites()
    }

    function vissza() {
        questionId--;
        kerdesBetoltes(questionId)
    }

    window.onload = function (e) {
        console.log("Oldal betöltve...");
        document.getElementById("előre_gomb").onclick = elore;
        document.getElementById("vissza_gomb").onclick = vissza;
        kerdesBetoltes(questionId)
        fetch('/questions/1')
            .then(response => response.json())
            .then(data => Kerdesmegjelenites(data));
    }

    function valasztas(n) {
        if (n != joValasz) {
            document.getElementById(`válasz${n}`).classList.add("rossz");
            document.getElementById(`válasz${joValasz}`).classList.add("jó");
        }
        else {
            document.getElementById(`válasz${joValasz}`).classList.add("jó");
        }
}

function init() {
    for (var i = 0; i < questionsInHotList; i++) {
        let q = {
            question: {},
            goodAnswers: 0
        }
        hotList[i] = q;
    }

    //Első kérdések letöltése
    for (var i = 0; i < questionsInHotList; i++) {
        kérdésBetöltés(nextQuestion, i);
        nextQuestion++;
    }
}

