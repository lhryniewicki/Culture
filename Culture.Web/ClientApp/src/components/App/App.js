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
          <div style={{ backgroundColor:"#e9ebee" }} >
              <Router>
                  <React.Fragment>
                  <NavBar removeToken={this.removeToken} />
                  <Switch>
                      <Route exact path="/" render={()=><EventsView/>}/>
                      <Route exact path="/konto/rejestracja" render={() => <Register />} />
                      <Route exact path="/konto/login" render={() => <Login setToken={this.setToken} />} />
                      <Route exact path="/wydarzenia/nowe" render={() => <EventForm />} />
                      <Route exact path="/konto/kalendarz" render={() => <CalendarView />} /> 
                      <Route exact path="/wydarzenie/szczegoly/:eventSlug" render={(props) => <EventDetailsView {...props} />} />
                  </Switch>
                      </React.Fragment>

              </Router>
              
          </div>
      
    );
  }
}
