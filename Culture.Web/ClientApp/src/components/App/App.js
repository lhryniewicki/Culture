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

  render() {
      return (
          <div>
              <NavBar />
              <Router>
                  <Switch>
                      <Route exact path="/" render={()=><EventsView/>}/>
                      <Route exact path="/register" render={() => <Register />} />
                      <Route exact path="/login" render={() => <Login />} />
                      <Route exact path="/create" render={() => <EventForm />} />
                      <Route exact path="/calendar" render={() => <CalendarView />} /> 
                      <Route exact path="/event/details/:eventId" render={() => <EventDetailsView />} />
                  </Switch>
              </Router>
          </div>
      
    );
  }
}
