import React from 'react';
import {signIn} from '../../api/AccountApi';
import { Redirect, Link } from 'react-router-dom';
import { createConnection } from '../../utils/signalRUtils';
import '../Login/Login.css';
import loginImage from '../../assets/login/loginbackground.png';


class Login extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            userName: '',
            password: '',
            redirect: false
        };
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSumbit = this.handleSumbit.bind(this);

    }

    handleInputChange(e) {
        this.setState({
            [e.target.name]: e.target.value
        });
    }

   async handleSumbit(e) {
        e.preventDefault();
       const token = await signIn(
            this.state.userName,
            this.state.password
       );

       this.props.setToken(token);
       createConnection();

    this.setState({redirect:true});
        
    }

renderRedirect = () => {
    if (this.state.redirect) {
      return <Redirect to='/' />
    }
}
    render() {
        return (
            <div className="container picContainer ">
                {this.renderRedirect()}
                <div className="visible-lg">
                <img src={loginImage} className="image-fluid" />
                 </div>
                    <form className="text-center border border-black col-md-4 col-md-offset-4 myForm" onSubmit={this.handleSumbit}>

                        <p className="h4 mb-4">Logowanie</p>

                        <input type="text" name="userName" value={this.state.userName} onChange={this.handleInputChange} className="form-control mb-4" placeholder="Nazwa użytkownika" required />

                        <input type="password" name="password" value={this.state.password} onChange={this.handleInputChange} className="form-control mb-4" placeholder="Hasło" required />
                        <button className="btn btn-info btn-block my-4" type="submit">Zaloguj</button>
                        <hr />
                        <div className="d-flex justify-content-around">

                            <p>Zapomniałeś hasła?{` `}
                                <Link to="/konto/przypomnij">Przypomnij</Link>
                            </p>

                            <p>Nie masz konta?{` `}
                            <Link to="/konto/rejestracja">Załóż</Link>
                            </p>
                        </div>

                    </form>
                
            </div>

        );
    }
}
export default Login;



