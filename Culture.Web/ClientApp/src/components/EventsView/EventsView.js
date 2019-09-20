import React from 'react';
import EventPost from '../EventPost/EventPost';
import SearchWidget from '../Widgets/SearchWidget';
import CategoriesWidget from '../Widgets/CategoriesWidget';
import { getPreviewEventList } from '../../api/EventApi'
import '../EventsView/EventsView.css';

class EventsView extends React.Component {


    constructor(props) {
        super(props);

        this.state = {
            events:[]
        };

        this.moreEvents = this.moreEvents.bind(this);
        this.displayEvents = this.displayEvents.bind(this);

    }
    async componentDidMount() {
        let eventList = await getPreviewEventList(0, null);
        this.setState({ events: eventList.events });
    }
    moreEvents(event) {
        event.preventDefault();
        //
    }
    displayEvents() {
        let items=[];
        this.state.events.map((element, index) => {
            let jsDate = new Date(Date.parse(element.creationDate));
            let jsDateFormatted = jsDate.getUTCDate() + "-" + (jsDate.getMonth() + 1) + "-" + jsDate.getFullYear() ;
            items.push(
                <EventPost
                    key={index}
                    createdBy={element.createdBy}
                    comments={element.comments}
                    commentsCount={element.commentsCount}
                    eventName={element.name}
                    id={element.id}
                    date={jsDateFormatted}
                    reactions={element.reactions}
                    reactionsCount={element.reactionsCount}
                    eventDescription={element.shortContent}
                    picture={element.image}
                />)
        });
        return items;
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

                            {this.displayEvents()}

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
