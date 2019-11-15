import React from 'react';
import { Map, TileLayer, Marker, Popup } from 'react-leaflet';
import { getMap, getEventMap } from '../../api/EventApi';
import '../MyMap/MyMap.css';
import { Link } from 'react-router-dom';


class MyMap extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            Markers: [], 
            position: [51.91916667, 19.13441667],
                zoom: 6
        }
    }

    componentDidMount() {

        this.loadMarkers();

    }

    async loadMarkers() {
        let data;
        console.log(this.props.eventId)
        if (this.props.eventId !== null && typeof this.props.eventId !== "undefined") {
            data = await getEventMap(this.props.eventId);
        }
        else {
            data = await getMap();

        }
        const items = data.map((item, index) => 
        {
            console.log(item.latitude)
            if (item.longitude !== null && item.latitude !== null) {
                return <Marker
                    key={index}
                    position={[item.latitude, item.longitude ]}
                >
                    <Popup>
                        {item.name} <br />
                        {item.address} <br />
                        <Link className="btn btn-primary" to={`/wydarzenie/szczegoly/${item.urlSlug}`}>Przejdź do wydarzenia</Link>
                    </Popup>
                </Marker>
            }
              
                   }
        );
        console.log(items);
        this.setState({ Markers: items });
    }


    render() {

        return (
            <div className="container">
                <Map style={{ height:"800px" }} center={this.state.position} zoom={this.state.zoom}>
                    <TileLayer
                        attribution='&amp;copy <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                    />
                    {this.state.Markers.map((item) => {
                        return item;
                    })}
                </Map>
                </div>
            
        )
    }
}
export default MyMap