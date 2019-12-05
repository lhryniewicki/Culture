import React from 'react';
import { Redirect, withRouter } from 'react-router';
import { Link } from 'react-router-dom';
import '../NavBar/NavBar.css';
import Notification from '../Notification/Notification';
import { getNotificationsNumber, getNotifications } from '../../api/NotificationApi';
import { userIsAuthenticated, getUserId, getToken, removeUserToken } from '../../utils/JwtUtils';
import { getConnection, createConnection } from '../../utils/signalRUtils';
class NavBar extends React.Component {


    constructor(props) {
        super(props);


        
        if (window.performance) {
            if (userIsAuthenticated()) {

                if (performance.navigation.type === 1) {

                    let connection = getConnection();

                    if ("undefined" === typeof connection) {
                        createConnection();
                        connection = getConnection();

                    }
                }
           
            }
        }
        this.state = {
            menu: false,
            redirect: false,
            notifications: [],
            notificationPage: 0,
            notificationsNumber: 0,
            isProcessingPage: null,
            fromSocket:false
        };

        this.toggleMenu = this.toggleMenu.bind(this);
        this.logOut = this.logOut.bind(this);

    }


    async componentDidMount() {
        if (userIsAuthenticated()) {
            const notificationsNumber = await getNotificationsNumber();
            this.setState({
                notificationsNumber: notificationsNumber
            });
            
        }
    }
   
    async  componentDidUpdate() {
        if (userIsAuthenticated()) {

            if (!this.state.fromSocket) {

                const notificationsNumber = await getNotificationsNumber();

                if (this.state.notificationsNumber !== notificationsNumber)
                    this.setState({
                        notificationsNumber: notificationsNumber
                    });

            }

            let connection = getConnection();

            if ("undefined" === typeof connection) {
                createConnection();
                connection = getConnection();
               
            }
            if ( typeof connection.methods.receivenotification ==="undefined" || connection.methods.receivenotification.length === 0 ) {

                connection.on("ReceiveNotification", async () => {
                    await this.setState({
                        notificationsNumber: this.state.notificationsNumber + 1,
                        fromSocket:true
                    });
                });
            }
            console.log(connection);

        }
    }

    unload = () => {
        removeUserToken();
    };

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
            this.loadNotifications();
        }
    }
        
    
    loadNotifications = async () => {
        console.log("zassane");
        const notifications = await getNotifications(this.state.notificationPage);
        const notificationsNumber = await getNotificationsNumber();

        const newNotificationState = this.state.notifications.concat(notifications.notifications);

        this.setState({
                    notificationsNumber: notificationsNumber ,
                    notifications: newNotificationState,
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
                            <li className="nav-item ">
                                <Link className="nav-link myFont " style={{ paddingTop:"0px" }} to="/mapa">
                                    <span className="fas fa-map-marked-alt mr-2 fa-2x mapsStyle" >
                                    </span>
                                    Mapa
                                </Link>
                                
                            </li>
                            <li className="nav-item ">
                                <Link className="nav-link myFont " style={{ paddingTop: "0px" }} to="/kalendarz">
                                    <span className="far fa-calendar-alt mr-2 fa-2x mapsStyle" >
                                    </span>
                                    Kalendarz
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
                                            <Link className="nav-link myFont" to={`/konto/${getUserId()}`}>Moje konto</Link>
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
