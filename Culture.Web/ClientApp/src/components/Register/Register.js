import React from 'react';
import '../Login/Login.css'

class Register extends React.Component {


    render() {
        return (
            <div className="container">

                <form className="text-center border border-gray p-5 col-md-4 col-md-offset-4 myForm" style={{ marginTop: '17%' }} >

                    <p className="h4 mb-4">Zarejestruj</p>

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

                    <hr/>
                    <p>Masz konto?{` `}
                        <a href="/login">Zaloguj</a>
                    </p>


                </form>
            </div>
            
        );
    }
}
export default Register;



