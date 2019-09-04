import React, { Component } from 'react';
import NavBar from '../NavBar/NavBar';
import { Route, BrowserRouter as Router, Switch } from "react-router-dom";
import EventsView from '../EventsView/EventsView';

export default class App extends Component {
  displayName = App.name

  render() {
      return (
          <div>
              <NavBar />
              <Router>
                  <Switch>
                      <Route exact path="/" render={()=><EventsView/>}/>

                  </Switch>
              </Router>
          </div>
      
    );
  }
}
