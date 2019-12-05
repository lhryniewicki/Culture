import React from 'react';
import '../Register/Register.css';
import connectedPeople from '../../assets/register/connected-people-registration.png';
import { register } from '../../api/AccountApi';
import { validatePasswordInput, validateEmail } from '../../utils/loginValidators';


class Register extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            errors: [],
            firstName:'',
            lastName: '',
            userName: '',
            password: '',
            confirmPassword:'',
            email: '',
            question: '',
            answer:''
        }
       
    }

    handleInputChange = (e) => {
        this.setState({[e.target.name]:e.target.value})
    }

    register = async (e) => {
        e.preventDefault();

        await this.setState({ errors: [] });
        await this.validatePassword();
        await this.validateEmail();


        if (this.state.errors.length === 0) {
                register({
                    firstName: this.state.firstName,
                    lastName: this.state.lastName,
                    userName: this.state.userName,
                    password: this.state.password,
                    email: this.state.email,
                    question: this.state.question,
                    answer: this.state.answer
                }).catch(e => { return e.json() })
                    .then(e => {
                        typeof e !== "undefined" ? 
                            this.setState({ errors: [e].concat(this.state.errors) })
                            :
                            null
                    }
                    
                );
        }

    }

    validatePassword = async () => {
        const errorsArray = validatePasswordInput(this.state.password, this.state.confirmPassword);
        console.log(errorsArray);
        await this.setState({ errors: this.state.errors.concat(errorsArray) });
    }

    validateEmail = async () => {
        const errorsArray = validateEmail(this.state.email);
        console.log(errorsArray);

        await this.setState({ errors: this.state.errors.concat(errorsArray)});
    }

    render() {
        return (
            <div className="container regBackground ">
                {this.state.errors.length > 0  ? this.state.errors.map((item) => (
                    <div className="alert alert-danger" role="alert">
                        {item}
                    </div>                   
                )
                ) :
                    null
                }
                <div className="mt-5 row">
                    <div className="col-md-6 ">
                        <img src={connectedPeople} className="img-responsive" />
                    </div>
                  
                    <form onSubmit={this.register} className="text-center  border border-gray col-md-4 pull-right  myRegForm" style={{ backgroundColor: "#efffed" }} >
                        <p className="h4 mb-4">Rejestracja</p>
                        <hr />
                        <input type="text" name="userName" className="form-control mb-4" onChange={this.handleInputChange} placeholder="Nazwa użytkownika" required />

                        <div className="form-row mb-4">
                            <div className="col">
                                <input type="text" id="defaultRegisterFormFirstName" onChange={this.handleInputChange} name="firstName" className="form-control" placeholder="Imię" required />
                            </div>
                            <div className="col">
                                <input type="text" id="defaultRegisterFormLastName" onChange={this.handleInputChange} name="lastName" className="form-control" placeholder="Nazwisko" required />
                            </div>
                        </div>

                        <input type="email" id="defaultRegisterFormEmail" onChange={this.handleInputChange} name="email" className="form-control mb-4" placeholder="E-mail" required/>

                        <input type="password" id="defaultRegisterFormPassword" onChange={this.handleInputChange} name="password" className="form-control" required placeholder="Hasło" aria-describedby="defaultRegisterFormPasswordHelpBlock" required />

                        <small id="defaultRegisterFormPasswordHelpBlock" className="form-text text-muted mb-4">
                            6 znaków, cyfra, wielka litera, znak specjalny
                    </small>
                        <input type="password" id="defaultRegisterFormPassword" name="confirmPassword" required onChange={this.handleInputChange} className="form-control mb-4" placeholder="Powtórz hasło" aria-describedby="defaultRegisterFormPasswordHelpBlock" required />


                        <input type="text" id="defaultRegisterPhonePassword" onChange={this.handleInputChange} name="question" required className="form-control" placeholder="Pytanie pomocnicze" aria-describedby="defaultRegisterFormPhoneHelpBlock" required />
                        <small id="defaultRegisterFormPhoneHelpBlock" className="form-text text-muted mb-4">
                            Przydatne podczas odzyskiwania hasła
                    </small>
                        <input type="text" id="defaultRegisterPhonePassword" name="answer" required onChange={this.handleInputChange}  className="form-control" placeholder="Odpowiedź" aria-describedby="defaultRegisterFormPhoneHelpBlock" required />

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



