import React from 'react';
import '../Register/Register.css';
import connectedPeople from '../../assets/register/connected-people-registration.png';
import { register } from '../../api/AccountApi';



class Register extends React.Component {

    constructor(props) {
        super(props);

       
    }

    handleInputChange = (e) => {
        this.setState({[e.target.name]:e.target.value})
    }

    register = (e) => {
        e.preventDefault();
        register({
            firstName: this.state.firstName,
            lastName: this.state.lastName,
            userName: this.state.userName,
            password: this.state.password,
            email: this.state.email,
            question: this.state.question,
            answer:this.state.answer
        })

    }
    render() {
        return (
            <div className="container regBackground ">
                <div className="mt-5 row">
                    <div className="col-md-6 ">
                        <img src={connectedPeople} className="img-responsive" />
                    </div>

                    <form onSubmit={this.register} className="text-center  border border-gray col-md-4 pull-right  myRegForm" >
                        <p className="h4 mb-4">Zarejestruj</p>
                        <hr />
                        <input type="text" name="userName" className="form-control mb-4" onChange={this.handleInputChange} placeholder="Nazwa użytkownika" />

                        <div className="form-row mb-4">
                            <div className="col">
                                <input type="text" id="defaultRegisterFormFirstName" onChange={this.handleInputChange} name="firstName" className="form-control" placeholder="Imię" required />
                            </div>
                            <div className="col">
                                <input type="text" id="defaultRegisterFormLastName" onChange={this.handleInputChange} name="lastName" className="form-control" placeholder="Nazwisko" />
                            </div>
                        </div>

                        <input type="email" id="defaultRegisterFormEmail" onChange={this.handleInputChange} name="email" className="form-control mb-4" placeholder="E-mail" />

                        <input type="password" id="defaultRegisterFormPassword" onChange={this.handleInputChange} name="password" className="form-control" placeholder="Hasło" aria-describedby="defaultRegisterFormPasswordHelpBlock" required />

                        <small id="defaultRegisterFormPasswordHelpBlock" className="form-text text-muted mb-4">
                            6 znaków, cyfra, wielka litera, znak specjalny
                    </small>
                        <input type="password" id="defaultRegisterFormPassword" name="confirmPassword" onChange={this.handleInputChange} className="form-control mb-4" placeholder="Powtórz hasło" aria-describedby="defaultRegisterFormPasswordHelpBlock" required />


                        <input type="text" id="defaultRegisterPhonePassword" onChange={this.handleInputChange} name="question" className="form-control" placeholder="Pytanie pomocnicze" aria-describedby="defaultRegisterFormPhoneHelpBlock" required />
                        <small id="defaultRegisterFormPhoneHelpBlock" className="form-text text-muted mb-4">
                            Przydatne podczas odzyskiwania hasła
                    </small>
                        <input type="text" id="defaultRegisterPhonePassword" name="answer" onChange={this.handleInputChange}  className="form-control" placeholder="Odpowiedź" aria-describedby="defaultRegisterFormPhoneHelpBlock" required />

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



