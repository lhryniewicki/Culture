import React from 'react';
import { Page, Text, View, Document, StyleSheet, Image } from '@react-pdf/renderer';

class EventPDF extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {

        const styles = StyleSheet.create({
           header:{
            textAlign:'center',
            marginBottom:'10px;',

            },
            info:{
            textAlign:'left',
            margin:'10px;',

            },
           myCard: {
                backgroundColor: 'white'
            },
            cursor: {
                cursor:'pointer'
            },
            showSpace: {
                whiteSpace:'preWrap',
                margin:'10px;',
                marginTop:'10px;',
                fontSize:'14'
            }
        });

        return (
            <Document

            >
                <Page size="A4" style={styles.page}>
                    <View style={styles.section}>
                        <Text style={styles.header}>{this.props.name}</Text>
                    </View>
                        <Image src={`${this.props.imagePath}`} />

                    <View style={styles.section}>
                        <Text style={styles.info}>

                        Miejsce odbycia: {this.props.takesPlaceDate} o {this.props.takesPlaceHour}, {this.props.streetName}, {this.props.cityName}
                        </Text>
                    </View>
                    <View style={styles.section}>
                        <Text style={styles.info}>
                        Cena wstepu: {this.props.price ===0 ? 'Darmo' : this.props.price + 'zl'}
                        </Text>
                    </View>
                    <View style={styles.section}>
                        <Text style={styles.showSpace}>{this.props.content}</Text>
                    </View>
                </Page>
            </Document>
        );
    }
}
export default EventPDF;
