import React from 'react';
import '../RemindPassView/RemindPassView.css';
import remindPass from '../../assets/remindPass/remindPassword.jpg';
import { getSecretQuestion, checkAnswer, sendPassword } from '../../api/AccountApi';
class RemindPassView extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            questionLock: true,
            passwordLock: true,
            userName: '',
            password: '',
            confirmPassword: '',
            answer: '',
            question: '',
            step:0
        };
    }

    onSubmit = async (e) => {
        e.preventDefault();
        if (this.state.step === 0) {
            const question = await getSecretQuestion(this.state.userName);
            this.setState({
                question: question,
                questionLock: false,
                step: 1
            });
        }
        else
            if (this.state.step === 1) {
                const answer = await checkAnswer(this.state.answer, this.state.userName);
                if (answer) {
                    this.setState({
                        passwordLock: false,
                        questionLock: true,
                        step: 2
                    });
                }
            }
            else {
                await sendPassword(this.state.password, this.state.userName);
            }

       
    }

    handleInputChange = (e)=> {
        this.setState({
            [e.target.name]: e.target.value
        });
    }
    render() {
        return (
            <div className="container regBackground">
                <div className="mt-5 row">
                    <div className="col-md-6 ">
                        <img src={remindPass} className="img-responsive" />
                    </div>

                    <form className="text-center  border border-gray col-md-4 pull-right  myRegForm" onSubmit={this.onSubmit} >

                        <p className="h4 mb-4">Zmiana hasła</p>
                        <hr />

                        <input type="text" className="form-control mb-4" name="userName" value={this.state.userName} onChange={this.handleInputChange} placeholder="Nazwa użytkownika" />

                        <input type="text" readOnly  value={this.state.question} className="form-control mb-4" placeholder="Pytanie pomocnicze" />

                        <input type="text" readOnly={this.state.questionLock} name="answer" value={this.state.answer} onChange={this.handleInputChange} className="form-control mb-4" placeholder="Odpowiedź" />

                        <input type="password" readOnly={this.state.passwordLock} name="password" value={this.state.password} onChange={this.handleInputChange}  className="form-control mb-4" placeholder="Hasło" />

                        <input type="password" readOnly={this.state.passwordLock} name="confirmPassword" value={this.state.confirmPassword} onChange={this.handleInputChange} className="form-control mb-4" placeholder="Powtórz hasło" />

                        <button className="btn btn-info my-4 btn-block" type="submit">Zmień hasło</button>

                    </form>
                </div>
                <div className="row mt-5 pl-5">

                    <p className="lead col-md-7">
                      W jaki sposób zmienić hasło? To proste!
                        </p>
                   
                    <div className="col-md-6 my-2">
                        <ul className="list-group list-group-flush">
                            <li className="myLi" >
                               1. Podaj swoją nazwę użytkownika i zatwierdź.
                            </li>
                        </ul>
                    </div>
                    <div className="col-md-6 my-2">
                        <ul className="list-group list-group-flush ">
                            <li className="myLi">
                               2. Odpowiedz na sekretne pytanie podawane przy rejestracji i zatwierdź.
                             </li>
                        </ul>
                    </div>

                    <div className="col-md-6 my-2">
                        <ul className="list-group list-group-flush">
                            <li className="myLi">
                               3. Po poprawnej odpowiedzi podaj nowe hasło.
                             </li>
                        </ul>
                    </div>
                    <div className="col-md-6 my-2">
                        <ul className="list-group list-group-flush">
                            <li className="myLi">
                                4. Twoje hasło zostanie zmienione!
                             </li>
                        </ul>
                    </div>
                </div>


            </div>
        );
    }
}
export default RemindPassView;



