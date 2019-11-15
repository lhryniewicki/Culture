import React from 'react';
import '../NotFoundView/NotFoundView.css';

class NotFoundView extends React.Component {

    constructor(props) {
        super(props);

    }

    render() {

        return (

            <body>
                <div id="notfound">
                    <div className="notfound">
                        <div className="notfound-404">
                            <h3>Oops! Strona nie została znaleziona.</h3>
                            <h1><span>4</span><span>0</span><span>4</span></h1>
                        </div>
                        <h2>Niestety ale strona o danym adresie nie istnieje.</h2>
                        <h2>Sprawdź poprawność adresu lub skontaktuj się z administracją.</h2>
                    </div>
                </div>
            </body>
        )
    }
}
export default NotFoundView