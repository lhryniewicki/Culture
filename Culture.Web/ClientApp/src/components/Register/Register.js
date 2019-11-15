import React from 'react';
import '../Register/Register.css';
import connectedPeople from '../../assets/register/connected-people-registration.png';
class Register extends React.Component {


    render() {
        return (
            <div className="container regBackground">
                <div className="mt-5 row">
                    <div className="col-md-6 ">
                        <img src={connectedPeople} className="img-responsive" />
                    </div>

                    <form className="text-center  border border-gray col-md-4 pull-right  myRegForm" >

                        <p className="h4 mb-4">Zarejestruj</p>
                        <hr />
                        <div className="form-row mb-4">
                            <div className="col">
                                <input type="text" id="defaultRegisterFormFirstName" className="form-control" placeholder="Imię" required />
                            </div>
                            <div className="col">
                                <input type="text" id="defaultRegisterFormLastName" className="form-control" placeholder="Nazwisko" />
                            </div>
                        </div>

                        <input type="email" id="defaultRegisterFormEmail" className="form-control mb-4" placeholder="E-mail" />

                        <input type="password" id="defaultRegisterFormPassword" className="form-control" placeholder="Hasło" aria-describedby="defaultRegisterFormPasswordHelpBlock" required />

                        <small id="defaultRegisterFormPasswordHelpBlock" className="form-text text-muted mb-4">
                            At least 8 characters and 1 digit
                    </small>
                        <input type="password" id="defaultRegisterFormPassword" className="form-control mb-4" placeholder="Powtórz hasło" aria-describedby="defaultRegisterFormPasswordHelpBlock" required />


                        <input type="text" id="defaultRegisterPhonePassword" className="form-control" placeholder="Pytanie pomocnicze" aria-describedby="defaultRegisterFormPhoneHelpBlock" required />
                        <small id="defaultRegisterFormPhoneHelpBlock" className="form-text text-muted mb-4">
                            Przydatne podczas odzyskiwania hasła
                    </small>
                        <input type="text" id="defaultRegisterPhonePassword" className="form-control" placeholder="Odpowiedź" aria-describedby="defaultRegisterFormPhoneHelpBlock" required />

                        <button className="btn btn-info my-4 btn-block" type="submit">Zarejestruj</button>

                        <hr />
                        <p>Masz konto?{` `}
                            <a href="/konto/login">Zaloguj</a>
                        </p>


                    </form>
                </div>
                <div className="row mt-5 pl-5">

                        <p className="lead">
                            Korzyści z zarejestrowania się w naszym portalu społecznościowym jest wiele, oto zaledwie kilka z nich:
                        </p>
                    <div className="col-md-6 my-2">
                        <ul className="list-group list-group-flush">
                            <li className="myLi" >
                                Zamieszczanie wydarzen
                             </li>
                        </ul>
                    </div>
                    <div className="col-md-6 my-2">
                    <ul className="list-group list-group-flush ">
                            <li className="myLi">
                               Komunikacja z uczestnikami wydarzen
                             </li>
                        </ul>
                        </div>                 

                    <div className="col-md-6 my-2">
                        <ul className="list-group list-group-flush">
                            <li className="myLi">
                                Osobisty terminarz z imprezami
                             </li>
                        </ul>
                    </div>
                    <div className="col-md-6 my-2">
                        <ul className="list-group list-group-flush">
                            <li className="myLi">
                                Nawiazesz nowe znajomosci
                             </li>
                        </ul>
                    </div> 
                </div>

                    
            </div>
        );
    }
}
export default Register;



