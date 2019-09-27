import React from 'react';
import { Redirect, withRouter } from 'react-router';
import { Link } from 'react-router-dom';
import '../NavBar/NavBar.css';

class NavBar extends React.Component{

    
    constructor(props) {
        super(props);

        this.state = {
            menu: false,
            redirect:false
        };

        this.toggleMenu = this.toggleMenu.bind(this);
        this.logOut = this.logOut.bind(this);

    }

    toggleMenu() {
        this.setState({ menu: !this.state.menu });
    }
    async logOut() {
        this.props.removeToken();
       await this.setState({ redirect: true });

    }
    render() {
        const show = (this.state.menu) ? "show" : "";
        const move = (this.state.menu) ? "pull-left" : "pull-right";

        return (
            <nav className="navbar navbar-expand-md   fixed-top affix myNavbar">
                <div className="container">
                    <Link className="navbar-brand myFont" to="/">MyCulture</Link>
                    <button className="navbar-toggler" type="button" onClick={this.toggleMenu} >
                        <span className="navbar-toggler-icon"/>
                    </button>
                    <div className={"collapse navbar-collapse " + show}>
                        <ul className={"navbar-nav ml-auto " + move}>
                            <li className="nav-item ">
                                <Link className="nav-link myFont" to="">Strona główna
                                </Link>
                            </li>
                            {
                                localStorage.getItem('token') === null
                                    ?
                                    <React.Fragment>
                                        <li className="nav-item">
                                            <Link className="nav-link myFont" to="/konto/login">Login</Link>
                                        </li>
                                        <li className="nav-item">
                                            <Link className="nav-link myFont" to="/konto/rejestracja">Rejestracja</Link>
                                        </li>
                                    </React.Fragment>  
                                    :
                                    <React.Fragment>
                                        <li className="nav-item">
                                            <Link className="nav-link myFont" to="#">Moje konto</Link>
                                        </li>
                                        <li className="nav-item">
                                            <Link className="nav-link myFont" to="#">Powiadomienia</Link>
                                        </li>
                                        <li className="nav-item">
                                            <Link className="nav-link myFont" to="" onClick={this.logOut}>Wyloguj</Link>
                                        </li>
                                    </React.Fragment>    
                            }
                        </ul>
                    </div>
                </div>
            </nav>


        );
    }
}
export default withRouter(NavBar);
