import React from 'react';
import { Redirect, withRouter } from 'react-router';
import { Link } from 'react-router-dom';
import '../NavBar/NavBar.css';
import Notification from '../Notification/Notification';
import { getNotificationsNumber,getNotifications } from '../../api/NotificationApi';
class NavBar extends React.Component{

    
    constructor(props) {
        super(props);

        this.state = {
            menu: false,
            redirect: false,
            notifications: [],
            notificationPage: 0,
            notificationsNumber: null,
            isProcessingPage: null
        };

        this.toggleMenu = this.toggleMenu.bind(this);
        this.logOut = this.logOut.bind(this);

    }


    async componentDidMount() {
        const notificationsNumber = await getNotificationsNumber();
        this.setState({
            notificationsNumber: notificationsNumber
        });
    }
    toggleMenu() {
        this.setState({ menu: !this.state.menu });
    }
    async logOut() {
        this.props.removeToken();
        await this.setState({ redirect: true });

    }
    displayNotifications = () => {
        return this.state.notifications.map((item) => {
            return <Notification
                content={item.content}
                date={item.date}
                redirect={item.redirectUrl}
            />;
        });
    }
    onScroll = async (e) => {
        e.preventDefault();
        if (this.state.isProcessingPage === this.state.notificationPage) return;

        const element = document.getElementsByClassName("dropdown-menu")[0];

        if ((element.offsetHeight + element.scrollTop >= element.scrollHeight)) {

            await this.setState({ isProcessingPage: this.state.notificationPage });
            const notifications = await getNotifications(this.state.notificationPage);
            const newNotificationState = this.state.notifications.concat(notifications.notifications);

          await  this.setState({
                notifications: newNotificationState,
              notificationPage: this.state.notificationPage + 1
            });
        }
    }
        
    
    loadNotifications = async () => {
        const notifications = await getNotifications(this.state.notificationPage);
                this.setState({
                    notifications: notifications.notifications,
                    notificationPage: this.state.notificationPage + 1
                });}
        
    render() {
        const show = (this.state.menu) ? "show" : "";
        const move = (this.state.menu) ? "pull-left" : "pull-right";

        return (
            <nav className="navbar navbar-expand-md   fixed-top affix myNavbar"> 
               
                <div className="container">
                    <Link className="navbar-brand myFont" to="/">MyCulture</Link>
                    <button className="navbar-toggler myToggler" type="button" onClick={this.toggleMenu} >
                        <span className="fas fa-bars"/>
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
                                        <div className="dropdown" onScroll={this.onScroll}> 
                                            <button className="dropdown-toggle bgInherit " type="button" data-toggle="dropdown">
                                                <i onClick={this.loadNotifications} className="fas fa-bell  myNotification" notifications={this.state.notificationsNumber} />
                                                </button>
                                            <ul className="dropdown-menu scrollable-menu pull-right">
                                                {this.displayNotifications()}   
                                            </ul>
                                           
                                        </div>
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
