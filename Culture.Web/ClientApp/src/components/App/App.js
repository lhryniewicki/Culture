import React, { Component } from 'react';
import NavBar from '../NavBar/NavBar';
import { Route, BrowserRouter as Router, Switch } from "react-router-dom";
import EventsView from '../EventsView/EventsView';
import Register from '../Register/Register';
import Login from '../Login/Login';
import EventForm from '../EventForm/EventForm';
import CalendarView from '../CalendarView/CalendarView';
import AllCalendar from '../AllCalendar/AllCalendar';
import EventDetailsView from '../EventDetailsView/EventDetailsView';
import MyAccountView from '../MyAccountView/MyAccountView';
import RemindPassView from '../RemindPassView/RemindPassView';
import MyMap from '../MyMap/MyMap';
import AllMap from '../AllMap/AllMap';
import { saveUserToken, removeUserToken, userIsAuthenticated, getToken } from '../../utils/JwtUtils';
import NotFoundView from '../NotFoundView/NotFoundView';

export default class App extends Component {
    displayName = App.name
    constructor(props) {
        super(props);
        if (!userIsAuthenticated()) removeUserToken();
        this.state = {
            token: getToken(),
            connection:false
        }
        this.removeToken = this.removeToken.bind(this);
        this.setToken = this.setToken.bind(this);

    }
    removeToken() {
        removeUserToken();
        this.setState({token:null});
    }
    setToken(token) {
        saveUserToken(token);
        this.setState({
            token: token,
            connection:true
        });
    }
  render() {
      return (
          <div style={{ backgroundColor:"#e9ebee" }} >
              <Router>
                  <React.Fragment>
                      <NavBar setConnection={this.state.connection} removeToken={this.removeToken} />
                  <Switch>
                          <Route exact path="/" render={() => <EventsView />} />
                          <Route exact path="/konto/przypomnij" render={() => <RemindPassView />} />
                      <Route exact path="/konto/rejestracja" render={() => <Register />} />
                          <Route exact path="/konto/login" render={() => <Login setToken={this.setToken} />} />
                          <Route exact path="/mapa" render={() => <AllMap />} />
                      <Route exact path="/konto/mapa" render={() => <MyMap />} />
                      <Route exact path="/wydarzenia/nowe" render={() => <EventForm />} />
                          <Route exact path="/konto/kalendarz" render={() => <CalendarView />} />
                          <Route exact path="/kalendarz" render={() => <AllCalendar />} />
                          <Route exact path="/wydarzenie/szczegoly/:eventSlug" render={(props) => <EventDetailsView {...props} />} />
                          <Route exact path="/konto/:userId" render={(props) => <MyAccountView {...props} />} />
                          <Route path="/" render={() => <NotFoundView />} />

                  </Switch>
                      </React.Fragment>

              </Router>
              
          </div>
      
    );
  }
}
