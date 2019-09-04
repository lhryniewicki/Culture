import React from 'react';


class NavBar extends React.Component{

    
    constructor(props) {
        super(props);

        this.state = {
            menu: false
        };

        this.toggleMenu = this.toggleMenu.bind(this);

    }

    toggleMenu() {
        this.setState({ menu: !this.state.menu });
    }


    render() {
        const show = (this.state.menu) ? "show" : "";
        const move = (this.state.menu) ? "pull-left" : "pull-right";

        return (


            <nav className="navbar navbar-expand-lg navbar-dark bg-dark fixed-top">
                <div className="container">
                    <a className="navbar-brand" href="#">MyCulture</a>
                    <button className="navbar-toggler" type="button" onClick={this.toggleMenu} >
                        <span className="navbar-toggler-icon"/>
                    </button>
                    <div className={"collapse navbar-collapse " + show}>
                        <ul className={"navbar-nav ml-auto " + move}>
                            <li className="nav-item ">
                                <a className="nav-link" href="#">Strona główna
                                </a>
                            </li>
                            {
                                localStorage.getItem('token') === null
                                    ?
                                    <React.Fragment>
                                        <li className="nav-item">
                                            <a className="nav-link" href="#">Login</a>
                                        </li>
                                        <li className="nav-item">
                                            <a className="nav-link" href="#">Rejestracja</a>
                                        </li>

                                    </React.Fragment>  
                                        
                                    :
                                    <li className="nav-item">
                                        <a className="nav-link" href="#">Moje konto</a>
                                    </li>
                            }
   
                            <li className="nav-item">
                                <a className="nav-link" href="#">Contact</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>


        );
    }
}
export default NavBar;
