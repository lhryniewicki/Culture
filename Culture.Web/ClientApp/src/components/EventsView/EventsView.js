import React from 'react';
import EventPost from '../EventPost/EventPost';
import SearchWidget from '../Widgets/SearchWidget';
import CategoriesWidget from '../Widgets/CategoriesWidget';

class EventsView extends React.Component {


    constructor(props) {
        super(props);

        this.state = {
        };


    }

    render() {
        return (
            <div className="container">
                <div className="row">
                    <div className="col-md-8">
                        <h1 className="my-4">Wydarzenia
                         <small> Ostatnio dodane</small>
                        </h1>


                        <EventPost />
                        <EventPost />
                        <EventPost />
                        <EventPost />
                        <EventPost />

                        <ul className="pagination justify-content-center mb-4">
                            <li className="page-item disabled">
                                <a className="page-link " href="#">&larr; Stare</a>
                            </li>
                            <li className="page-item ">
                                <a className="page-link" href="#">Nowe &rarr;</a>
                            </li>
                        </ul>
                    </div>
                    <div className="col-md-4">
                        <SearchWidget />
                        <CategoriesWidget/>
                    </div>


                </div>

            </div>


        );
    }
}
export default EventsView;
