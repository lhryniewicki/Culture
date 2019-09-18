import React, { Component } from 'react';
import NavBar from '../NavBar/NavBar';
import { Route, BrowserRouter as Router, Switch } from "react-router-dom";
import EventsView from '../EventsView/EventsView';
import Register from '../Register/Register';
import Login from '../Login/Login';
import EventForm from '../EventForm/EventForm';
import CalendarView from '../CalendarView/CalendarView';
import EventDetailsView from '../EventDetailsView/EventDetailsView'

export default class App extends Component {
    displayName = App.name
    constructor(props) {
        super(props);

        this.state = {
            token: localStorage.getItem('token')
        }
        this.removeToken = this.removeToken.bind(this);
        this.setToken = this.setToken.bind(this);

    }
    removeToken() {
        localStorage.removeItem("token");
        this.setState({token:null});
    }
    setToken(token) {
        localStorage.setItem("token",token);
        this.setState({ token: token });
    }
  render() {
      return (
          <div>
              <NavBar removeToken={this.removeToken} />
              <Router>
                  <Switch>
                      <Route exact path="/" render={()=><EventsView/>}/>
                      <Route exact path="/account/register" render={() => <Register />} />
                      <Route exact path="/account/login" render={() => <Login setToken={this.setToken} />} />
                      <Route exact path="/event/create" render={() => <EventForm />} />
                      <Route exact path="/account/calendar" render={() => <CalendarView />} /> 
                      <Route exact path="/event/details/:eventId" render={() => <EventDetailsView />} />
                  </Switch>
              </Router>
          </div>
      
    );
  }
}
