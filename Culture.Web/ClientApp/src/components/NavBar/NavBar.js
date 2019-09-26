import React from 'react';
import { Redirect } from 'react-router';
import { Link } from 'react-router-dom';

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
            <nav className="navbar navbar-expand-md navbar-dark bg-info fixed-top affix">
                <div className="container">
                    <a className="navbar-brand" href="">MyCulture</a>
                    <button className="navbar-toggler" type="button" onClick={this.toggleMenu} >
                        <span className="navbar-toggler-icon"/>
                    </button>
                    <div className={"collapse navbar-collapse " + show}>
                        <ul className={"navbar-nav ml-auto " + move}>
                            <li className="nav-item ">
                                <Link className="nav-link" to="">Strona główna
                                </Link>
                            </li>
                            {
                                localStorage.getItem('token') === null
                                    ?
                                    <React.Fragment>
                                        <li className="nav-item">
                                            <Link className="nav-link" to="/konto/login">Login</Link>
                                        </li>
                                        <li className="nav-item">
                                            <Link className="nav-link" to="/konto/rejestracja">Rejestracja</Link>
                                        </li>

                                    </React.Fragment>  
                                        
                                    :
                                    <React.Fragment>
                                        <li className="nav-item">
                                            <Link className="nav-link" to="#">Moje konto</Link>
                                        </li>
                                        <li className="nav-item">
                                            <Link className="nav-link" to="#">Powiadomienia</Link>
                                        </li>
                                        <li className="nav-item">
                                            <Link className="nav-link" to="" onClick={this.logOut}>Wyloguj</Link>
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
export default NavBar;
