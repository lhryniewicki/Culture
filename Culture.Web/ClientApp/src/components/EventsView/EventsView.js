import React from 'react';
import EventPost from '../EventPost/EventPost';
import SearchWidget from '../Widgets/SearchWidget';
import CategoriesWidget from '../Widgets/CategoriesWidget';
import Shortcuts from '../Shortcuts/Shortcuts';
import '../EventsView/EventsView.css';

class EventsView extends React.Component {


    constructor(props) {
        super(props);

        this.state = {
            events:[]
        };

        this.moreEvents = this.moreEvents.bind(this);
    }

    moreEvents(event) {
        event.preventDefault();
        //
    }
    render() {
        return (
            <div>

                <div className="container">

                    <div className="row">

                        <div className="col-md-8">

                            <h1 className="my-4 text-center">Wydarzenia
                        <small> Ostatnio dodane</small>
                            </h1>


                            <EventPost />
                            <EventPost />
                            <EventPost />
                            <EventPost />
                            <EventPost />

                            <ul className="pagination justify-content-center mb-4">
                                <li className="page-item">
                                    <a className="page-link " onClick={this.moreEvents} href="#">&darr; Więcej</a>
                                </li>
                            </ul>
                        </div>
                        <div className="col-md-3 fixed" >
                            <div className="affix">
                                <SearchWidget />
                                <CategoriesWidget />
                            </div>

                        </div>


                    </div>


                </div>

                </div>
               

        );
    }
}
export default EventsView;
